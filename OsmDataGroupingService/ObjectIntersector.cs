using Gems.AddressRegistry.OsmDataParser.Model;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Gems.AddressRegistry.OsmDataGroupingService;

public class ObjectIntersector
{
    public bool Intersects(RealObject realObject1, RealObject realObject2)
    {
        var reader = new GeoJsonReader();
        var firstGeometry = reader.Read<FeatureCollection>(realObject1.GeoJson);
        var secondGeometry = reader.Read<FeatureCollection>(realObject2.GeoJson);

        var firstFeature = firstGeometry.FirstOrDefault();
        var secondFeature = secondGeometry.FirstOrDefault();
        
        return firstFeature!.Geometry.Contains(secondFeature!.Geometry);
    }
}