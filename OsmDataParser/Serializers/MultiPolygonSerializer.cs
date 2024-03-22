using System.Text.Encodings.Web;
using System.Text.Json;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Serializers;

public static class MultiPolygonSerializer
{
    private static readonly Dictionary<long, Node> NodeCache = new Dictionary<long, Node>();
    
    public static string Serialize(RealObject realObject, OsmData osmData)
    {
        if (realObject == null || realObject.GetType() == typeof(Street))
            throw new ArgumentException("Object can not be serialized");
        
        var features = new List<Feature>();
        var ways = realObject.Components;
        var totalBorder = new List<List<LineString>>();

        foreach (var way in ways)
        {
            var coordinates = new List<Position>();
            var wayNodes = new List<Node>();
            
            foreach (var nodeId in way.Nodes)
            {
                if (NodeCache.TryGetValue(nodeId, out var cachedNode))
                    wayNodes.Add(cachedNode);
            
                var wayNode = osmData.Nodes.FirstOrDefault(node => node.Id == nodeId);

                if (wayNode != null)
                {
                    wayNodes.Add(wayNode);
                    NodeCache[nodeId] = wayNode;
                }
            }

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
            { "ObjectName", realObject.Name }
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
        
        return geoJson;
    }
}