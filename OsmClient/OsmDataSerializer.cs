using osmDataParser.model;
using System.Text.Encodings.Web;
using System.Text.Json;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using OsmDataParser.Support;

namespace osm_client;

public class OsmDataSerializer
{
    public string SerializeDistrict(District district, OsmData osmData)
    {
        var features = new List<Feature>();
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
        
        return geoJson;
    }
}