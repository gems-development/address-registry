using System.Text.Encodings.Web;
using System.Text.Json;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;

namespace Gems.AddressRegistry.OsmDataGroupingService.Serializers;

public class MultiLineSerializer
{
    public static string Serialize(RealObject realObject, OsmData osmData)
    {
        if (realObject == null || realObject.GetType() == typeof(District)
                               || realObject.GetType() == typeof(Settlement)
                               || realObject.GetType() == typeof(City))
            throw new ArgumentException("Object can not be serialized");
        
        var features = new List<Feature>();
        var ways = realObject.Components;
        var totalStreet = new List<LineString>();

        foreach (var way in ways)
        {
            var coordinates = new List<Position>();
            var wayNodeIds = way.Nodes.ToHashSet();
            var wayNodes = wayNodeIds
                .Select(id => osmData.Nodes.FirstOrDefault(node => node.Id == id))
                .Where(node => node != null)
                .ToList();
            
            if (wayNodes.Count < 2)
                continue;

            if (wayNodes.Any())
            {
                foreach (var node in wayNodes)
                    coordinates.Add(new Position((double)node.Latitude!, (double)node.Longitude!));
            }
            
            var streetPart = new LineString(coordinates);
            totalStreet.Add(streetPart);
        }

        var properties = new Dictionary<string, object>
        {
            { "ObjectName", realObject.Name }
        };
        
        
        var multiLine = new MultiLineString(totalStreet);

        var feature = new Feature(multiLine, properties);
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