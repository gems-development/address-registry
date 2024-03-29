using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Gems.AddressRegistry.OsmDataGroupingService;

public class ObjectGrouper
{
    public bool Group(string geoJson1, string geoJson2)
    {
        var reader = new GeoJsonReader();
        var firstGeometry = reader.Read<FeatureCollection>(geoJson1);
        var secondGeometry = reader.Read<FeatureCollection>(geoJson2);

        var firstFeature = firstGeometry.FirstOrDefault();
        var secondFeature = secondGeometry.FirstOrDefault();
        
        return firstFeature!.Geometry.Contains(secondFeature!.Geometry);
    }
}