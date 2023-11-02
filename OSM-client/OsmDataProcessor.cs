using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using OsmSharp;
using OsmSharp.Streams;

namespace OSM_client;

public class OsmDataProcessor
{
    private List<Node> _nodes = new List<Node>();
    private List<Way> _ways = new List<Way>();
    private List<Relation> _relations = new List<Relation>();
    
    public void ProcessOsmData(string path)
    {
        var outputFilePath = "streets.geojson";
        var fileStream = new FileInfo(path).OpenRead();
        var osmStreamSource = new XmlOsmStreamSource(fileStream);
        
        // сортируем объекты OSM-модели по категориям
        foreach (var element in osmStreamSource)
        {
            if (element.Type is OsmGeoType.Node)
                _nodes.Add((Node) element);
            
            else if (element.Type is OsmGeoType.Way)
                _ways.Add((Way) element);
            
            else if (element.Type is OsmGeoType.Relation)
                _relations.Add((Relation) element);
        }

        // Выводим геометрию улиц в GeoJson
        var streets = GetStreetList(_ways);
        var streetCoordinates = new List<Position>();
        var features = new List<Feature>();
        foreach (var street in streets)
        {
            foreach (var nodeId in street.Nodes)
            {
                foreach (var node in _nodes.Where(node => node.Id == nodeId))
                    streetCoordinates.Add(new Position((double) node.Latitude, (double) node.Longitude));
            }
            
            var properties = new Dictionary<string, object>
            {
                { "StreetName", street.Tags["name"] }
            };
                
            var lineString = new LineString(streetCoordinates);
            var feature = new Feature(lineString, properties);
            features.Add(feature);
        }
        
        var featureCollection = new FeatureCollection(features);
        var geoJson = JsonConvert.SerializeObject(featureCollection, Formatting.Indented);
        
        File.WriteAllText(outputFilePath, geoJson);

        Console.WriteLine("Геометрия улиц записана в формате GeoJSON.");
    }

    // Получаем список всех улиц
    private List<Way> GetStreetList(List<Way> ways)
    {
        var streets = new List<Way>();
        foreach (var way in ways)
        {
            if (way.Tags.ContainsKey("highway") && way.Tags.ContainsKey("name"))
                streets.Add(way);
        }

        var streetGroup = streets.GroupBy(p => p.Tags["name"]);
        
        foreach (var street in streetGroup)
        {
            
        }
        
        return streets;
    }
}