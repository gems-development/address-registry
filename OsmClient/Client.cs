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
        
        var houseParser = OsmParserFactory.Create<House>();
        var houses = houseParser.ParseAll(osmData, "Пионерская улица");
        var features = new List<Feature>();
        
        foreach (var house in houses)
        {
            var houseGeoJson = MultiPolygonSerializer.Serialize(house, osmData);
            var houseGeometry = JsonSerializer.Deserialize<FeatureCollection>(houseGeoJson);
            var houseFeature = houseGeometry!.Features.First();
            features.Add(houseFeature);
            Console.WriteLine($"Объект с адресом {house.Name}, " +
                              $"д. {house.Number} записывается в формат GeoJson.");
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