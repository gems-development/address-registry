using System.Text.Encodings.Web;
using System.Text.Json;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using Gems.AddressRegistry.OsmDataGroupingService;
using Gems.AddressRegistry.OsmDataGroupingService.Serializers;
using Gems.AddressRegistry.OsmDataParser;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;
using OsmSharp.Streams;

namespace Gems.AddressRegistry.OsmClient;

public class Client
{
    private const string PathToPbf = "RU-YEV.osm.pbf";
    private const string OutputFilePath = "data.geojson";

    public static async Task Main(string[] args)
    {
        var osmData = await GetSortedOsmData();
        
        var grouper = new CityAndStreetGrouper();
        var streetParser = OsmParserFactory.Create<Street>();
        var cityParser = OsmParserFactory.Create<City>();
        
        var city = cityParser.Parse(osmData, "Биробиджан", null);
        var cityGeoJson = MultiPolygonSerializer.Serialize(city, osmData);

        var streets = streetParser.ParseAll(osmData, null);
        var features = new List<Feature>();
        
        foreach (var street in streets)
        {
            var streetGeoJson = MultiLineSerializer.Serialize(street, osmData);
            if (grouper.Group(cityGeoJson, streetGeoJson))
            {
                var streetGeometry = JsonSerializer.Deserialize<FeatureCollection>(streetGeoJson);
                var streetFeature = streetGeometry.Features.First();
                features.Add(streetFeature);
                Console.WriteLine("Объект {" + street.Name + "} записывается в формат GeoJSON.");
            }
        }
        
        var featureCollection = new FeatureCollection(features);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        
        var geoJson = JsonSerializer.Serialize(featureCollection, options);
        await File.WriteAllTextAsync(OutputFilePath, geoJson);
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
}