using System.Text.Encodings.Web;
using System.Text.Json;
using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Support;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Serializers;

public class MultiPolygonSerializer : IOsmToGeoJsonConverter
{
    private readonly Dictionary<long, Node> _nodeCache = new Dictionary<long, Node>();
    
    public string Serialize(List<Way> components, string objectName, OsmData osmData)
    {
        var features = new List<Feature>();
        var totalBorder = new List<LineString>();

        foreach (var way in components)
        {
            var coordinates = new List<Position>();
            var wayNodes = new List<Node>();
            
            foreach (var nodeId in way.Nodes)
            {
                if (_nodeCache.TryGetValue(nodeId, out var cachedNode))
                {
                    wayNodes.Add(cachedNode);
                }
                else
                {
                    var wayNode = osmData.Nodes.FirstOrDefault(node => node.Id == nodeId);
                    if (wayNode != null)
                    {
                        wayNodes.Add(wayNode);
                        _nodeCache[nodeId] = wayNode;
                    }
                }
            }

            if (wayNodes.Any())
            {
                foreach (var node in wayNodes)
                    coordinates.Add(new Position((double)node.Latitude!, (double)node.Longitude!));
            }

            if (coordinates.Count >= 2)
            {
                var borderPart = new LineString(coordinates);
                totalBorder.Add(borderPart);
            }
        }

        var properties = new Dictionary<string, object>
        {
            { "ObjectName", objectName }
        };

        var polygonList = new List<Polygon>();
        var borderLines = totalBorder.Where(borderPart 
            => borderPart.Coordinates.Count >= 4).ToList();

        var polygon = new Polygon(borderLines);
        polygonList.Add(polygon);

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
        
        return geoJson;
    }
}