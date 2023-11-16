using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using OsmDataParser;
using OsmSharp;
using OsmSharp.Streams;

namespace OSM_client;

public class Client
{
    private const string Path = "osm_data.xml";
    private const string OutputFilePath = "streets.geojson";
    

    public static async Task Main(string[] args)
    {
        var osmData = await GetSortedOsmDataFromXml();
        var streetDataParser = new StreetDataParser();
        var streets = streetDataParser.GetStreets(osmData);
        
        var streetCoordinates = new List<Position>();
        var features = new List<Feature>();

        foreach (var street in streets)
        {
            foreach (var way in street.Components)
            {
                foreach (var nodeId in way.Nodes)
                {
                    foreach (var node in osmData.Nodes.Where(node => node.Id == nodeId))
                        streetCoordinates.Add(new Position((double) node.Latitude, (double) node.Longitude));
                }
            }
            
            var properties = new Dictionary<string, object>
            {
                { "StreetName", street.Name }
            };
                
            var lineString = new LineString(streetCoordinates);
            var feature = new Feature(lineString, properties);
            features.Add(feature);
        }
        
        var featureCollection = new FeatureCollection(features);
        var geoJson = JsonConvert.SerializeObject(featureCollection, Formatting.Indented);
        
        await File.WriteAllTextAsync(OutputFilePath, geoJson);

        Console.WriteLine("Геометрия улиц записана в формате GeoJSON.");
    }

    private static async Task<OsmData> GetSortedOsmDataFromXml()
    {
        var osmData = new OsmData();
            
        var xmlData = await GetXmlFromOverpassApi();
        await File.WriteAllTextAsync(Path, xmlData);
        var fileStream = new FileInfo(Path).OpenRead();
        var osmStreamSource = new XmlOsmStreamSource(fileStream);
            
        // сортируем объекты OSM-модели по категориям
        foreach (var element in osmStreamSource)
        {
            if (element.Type is OsmGeoType.Node)
                osmData.Nodes.Add((Node) element);
            
            else if (element.Type is OsmGeoType.Way)
                osmData.Ways.Add((Way) element);
            
            else if (element.Type is OsmGeoType.Relation)
                osmData.Relations.Add((Relation) element);
        }
        
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