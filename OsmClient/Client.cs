using System.Text.Encodings.Web;
using System.Text.Json;
using Gems.AddressRegistry.OsmDataGroupingService;
using Gems.AddressRegistry.OsmDataGroupingService.Serializers;
using Gems.AddressRegistry.OsmDataParser;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using GeoJSON.Text.Feature;
using OsmSharp;
using OsmSharp.Streams;

namespace Gems.AddressRegistry.OsmClient;

public class Client
{
    private const string PathToPbf = "RU-YEV.osm.pbf";
    private const string OutputFilePath = "data.geojson";
    private const string AreaName = "Еврейская автономная область";

    public static async Task Main(string[] args)
    {
        var osmData = await GetSortedOsmData();
        
        var grouper = new ObjectGrouper();
        var areaParser = OsmParserFactory.Create<Area>();
        var districtParser = OsmParserFactory.Create<District>();
        var settlementParser = OsmParserFactory.Create<Settlement>();
        var cityParser = OsmParserFactory.Create<City>();
        var streetParser = OsmParserFactory.Create<Street>();
        var houseParser = OsmParserFactory.Create<House>();

        var features = new List<Feature>();
        var area = areaParser.Parse(osmData, AreaName, default!);
        var districts = districtParser.ParseAll(osmData, AreaName);
        var settlements = settlementParser.ParseAll(osmData, AreaName);
        var cities = cityParser.ParseAll(osmData, null!);
        var streets = streetParser.ParseAll(osmData, null!);
        
        var areaGeoJson = MultiPolygonSerializer.Serialize(area, osmData);
        var areaGeometry = JsonSerializer.Deserialize<FeatureCollection>(areaGeoJson);
        var areaFeature = areaGeometry!.Features.First();
        features.Add(areaFeature);
        Console.WriteLine("Объект {" + area.Name + "} записался в формат GeoJSON.");

        foreach (var district in districts)
        {
            var districtGeoJson = MultiPolygonSerializer.Serialize(district, osmData);
            var districtGeometry = JsonSerializer.Deserialize<FeatureCollection>(districtGeoJson);
            var districtFeature = districtGeometry!.Features.First();
            features.Add(districtFeature);
            Console.WriteLine("Объект {" + district.Name + "} записался в формат GeoJSON.");
        }
        
        foreach (var settlement in settlements)
        {
            var settlementGeoJson = MultiPolygonSerializer.Serialize(settlement, osmData);
            var settlementGeometry = JsonSerializer.Deserialize<FeatureCollection>(settlementGeoJson);
            var settlementFeature = settlementGeometry!.Features.First();
            features.Add(settlementFeature);
            Console.WriteLine("Объект {" + settlement.Name + "} записался в формат GeoJSON.");
        }

        foreach (var city in cities)
        {
            var cityGeoJson = MultiPolygonSerializer.Serialize(city, osmData);
            if (grouper.Group(areaGeoJson, cityGeoJson))
            {
                var cityGeometry = JsonSerializer.Deserialize<FeatureCollection>(cityGeoJson);
                var cityFeature = cityGeometry!.Features.First();
                features.Add(cityFeature);
                Console.WriteLine("Объект {" + city.Name + "} записался в формат GeoJSON.");
                
                foreach (var street in streets)
                {
                    var streetGeoJson = MultiLineSerializer.Serialize(street, osmData);
                    if (grouper.Group(cityGeoJson, streetGeoJson))
                    {
                        var streetGeometry = JsonSerializer.Deserialize<FeatureCollection>(streetGeoJson);
                        var streetFeature = streetGeometry!.Features.First();
                        features.Add(streetFeature);
                        Console.WriteLine("Объект {" + street.Name + "} записался в формат GeoJSON.");
                        
                        var houses = houseParser.ParseAll(osmData, street.Name);
                        foreach (var house in houses)
                        {
                            var houseGeoJson = MultiPolygonSerializer.Serialize(house, osmData);
                            if (grouper.Group(cityGeoJson, houseGeoJson))
                            {
                                var houseGeometry = JsonSerializer.Deserialize<FeatureCollection>(houseGeoJson);
                                var houseFeature = houseGeometry!.Features.First();
                                features.Add(houseFeature);
                                Console.WriteLine($"Объект с адресом {house.Name}, " +
                                                  $"д. {house.Number} записался в формат GeoJson.");
                            }
                        }
                    }
                }
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