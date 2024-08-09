using Gems.AddressRegistry.OsmDataParser.Model;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Gems.AddressRegistry.OsmDataParser.DataGroupingServices
{
    public static class ObjectIntersector
    {
        private static readonly GeoJsonReader _geoJsonReader;

        private const double RequiredPercentageOfHitsInTheArea = 0.9;

        static ObjectIntersector() => _geoJsonReader = new GeoJsonReader();

        public static bool Intersects(RealObject realObject1, RealObject realObject2)
        {
            var firstGeometry = _geoJsonReader.Read<FeatureCollection>(realObject1.GeoJson);
            var secondGeometry = _geoJsonReader.Read<FeatureCollection>(realObject2.GeoJson);
            var firstFeature = firstGeometry?.FirstOrDefault();
            var secondFeature = secondGeometry?.FirstOrDefault();

            if (firstFeature is null || secondFeature is null)
                return false;

            if (firstFeature.Geometry.Intersects(secondFeature.Geometry))
            {
                Geometry intersectionArea;
                if(firstFeature.Geometry.Boundary.NumGeometries > 1)
                    intersectionArea = secondFeature.Geometry.Difference(firstFeature.Geometry);
                else
                    intersectionArea = secondFeature.Geometry.Intersection(firstFeature.Geometry);
                if(intersectionArea.Area/secondFeature.Geometry.Area > RequiredPercentageOfHitsInTheArea)
                    return true;
                return false;
            }

            return false;
        }
    }
}
