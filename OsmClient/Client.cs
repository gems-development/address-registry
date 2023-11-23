using System.Text.Encodings.Web;
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
    private const string OutputFilePath = "streets.geojson";

    public static async Task Main(string[] args)
    {
        var osmData = await GetSortedOsmData();
        var osmDataParser = new OsmDataParser();
        var localities = osmDataParser.GetLocalities(osmData);

        var borderCoordinates = new List<Position>();
        var features = new List<Feature>();

        foreach (var locality in localities)
        {
            //var localityMemberIds = locality.Components.Members.Select(o => o.Id).ToHashSet();
            //var localityWays = osmData.Ways.Where(way => localityMemberIds.Contains(way.Id ?? -1)).ToArray();
            var localityWays = locality.Components;

            foreach (var way in localityWays)
            {
                var wayNodeIds = way.Nodes.ToHashSet();
                var wayNodes = osmData.Nodes.Where(node => wayNodeIds.Contains(node.Id ?? -1)).ToArray();
                
                foreach (var node in wayNodes)
                    borderCoordinates.Add(new Position((double)node.Latitude!, (double)node.Longitude!));
            }
            
            var properties = new Dictionary<string, object>
            {
                { "LocalityName", locality.Name }
            };

            var lineString = new LineString(borderCoordinates);
            var feature = new Feature(lineString, properties);
            features.Add(feature);

            var featureCollection = new FeatureCollection(features);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var geoJson = JsonSerializer.Serialize(featureCollection, options);

            await File.WriteAllTextAsync(OutputFilePath, geoJson);
            Console.WriteLine("Граница {" + locality.Name + "} записана в формате GeoJSON.");
        }
    }

    private static async Task<OsmData> GetSortedOsmData()
    {
        var osmData = new OsmData();

        // var xmlData = await GetXmlFromOverpassApi();
        // await File.WriteAllTextAsync(PathToXml, xmlData);
        // var fileStream = new FileInfo(PathToXml).OpenRead();
        // var osmStreamSource = new XmlOsmStreamSource(fileStream);

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

        // Запрос к Overpass API
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