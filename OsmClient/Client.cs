﻿using System.Text.Encodings.Web;
using System.Text.Json;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using osmDataParser;
using OsmSharp;
using OsmSharp.Streams;

namespace osm_client;

public class Client
{
    private const string PathToPbf = "RU-OMS.osm.pbf";
    private const string OutputFilePath = "data.geojson";

    public static async Task Main(string[] args)
    {
        var osmData = await GetSortedOsmData();
        var osmDataParser = new OsmDataParser();
        var districts = osmDataParser.GetDistricts(osmData, "Омская область");
        var features = new List<Feature>();

        foreach (var district in districts)
        {
            var districtWays = district.Components;
            var totalBorder = new List<List<LineString>>();

            foreach (var way in districtWays)
            {
                var coordinates = new List<Position>();
                var wayNodeIds = way.Nodes.ToHashSet();
                var wayNodes = wayNodeIds
                    .Select(id => osmData.Nodes.FirstOrDefault(node => node.Id == id))
                    .Where(node => node != null)
                    .ToList();

                if (wayNodes.Any())
                {
                    var firstNode = wayNodes.First();
                    wayNodes.Add(firstNode);
                    
                    foreach (var node in wayNodes)
                        coordinates.Add(new Position((double)node.Latitude!, (double)node.Longitude!));
                }
                
                var borderPart = new List<LineString> { new LineString(coordinates) };
                totalBorder.Add(borderPart);
            }
            
            var properties = new Dictionary<string, object>
            {
                { "LocalityName", district.Name }
            };
            
            var polygonList = new List<Polygon>();
            foreach (var borderPart in totalBorder)
            {
                var polygon = new Polygon(borderPart);
                polygonList.Add(polygon);
            }

            var multiPolygon = new MultiPolygon(polygonList);
            
            var feature = new Feature(multiPolygon, properties);
            features.Add(feature);

            var featureCollection = new FeatureCollection(features);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var geoJson = JsonSerializer.Serialize(featureCollection, options);

            await File.WriteAllTextAsync(OutputFilePath, geoJson);
            Console.WriteLine("Граница {" + district.Name + "} записана в формате GeoJSON.");
        }
    }

    private static async Task<OsmData> GetSortedOsmData()
    {
        var osmData = new OsmData();
        await using var fileStream = new FileInfo(PathToPbf).OpenRead();
        using var osmStreamSource = new PBFOsmStreamSource(fileStream);

        // сортируем объекты OSM-модели по категориям
        foreach (var element in osmStreamSource)
            if (element.Type is OsmGeoType.Node)
                osmData.Nodes.Add((Node)element);

            else if (element.Type is OsmGeoType.Way)
                osmData.Ways.Add((Way)element);

            else if (element.Type is OsmGeoType.Relation)
                osmData.Relations.Add((Relation)element);

        return osmData;
    }

    private static Task<string> GetXmlFromOverpassApi()
    {
        const string url = "https://overpass-api.de/api/interpreter";
        var overpassClient = new OverpassApiClient(url);
        
        const string query = "[out:xml];" +
                             "\nnode(54.979788, 73.414227, 54.983705, 73.423204);" +
                             "\nout body;" +
                             "\nway(54.979788, 73.414227, 54.983705, 73.423204);" +
                             "\nout body;" +
                             "\nrelation(54.979788, 73.414227, 54.983705, 73.423204);" +
                             "\nout body;";

        return overpassClient.ExecuteOverpassQueryAsync(query);
    }
}