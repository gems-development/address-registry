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

            Geometry newFeature;

            if (firstFeature.Geometry.Boundary.NumGeometries == 1 && secondFeature.Geometry.Boundary.NumGeometries == 1)
            {
                if (firstFeature.Geometry.Intersects(secondFeature.Geometry))
                {
                    if (secondFeature.Geometry.Intersection(firstFeature.Geometry).Area / secondFeature.Geometry.Area > 0.9)
                        return true;
                    return false;
                }
                return false;
            }

            else if (firstFeature.Geometry.Boundary.NumGeometries > 1 && secondFeature.Geometry.Boundary.NumGeometries == 1)
            {
                var polygonsList = new List<Polygon>();
                Polygon largestPolygon = null;

                foreach (var element in (firstFeature.Geometry.Boundary as MultiLineString).Geometries)
                {
                    var newPolygon = Polygon.DefaultFactory.CreatePolygon(element.Coordinates);
                    polygonsList.Add(newPolygon);
                    if (largestPolygon == null || largestPolygon.Area < newPolygon.Area)
                        largestPolygon = newPolygon;
                }

                polygonsList.Remove(largestPolygon);
                var cuttingPolygons = new List<Polygon>();

                foreach (var element in polygonsList)
                {
                    if (largestPolygon.Covers(element))
                        cuttingPolygons.Add(element);
                    else largestPolygon.Union(element);
                }

                if (largestPolygon.Intersects(secondFeature.Geometry))
                {
                    foreach (var element in cuttingPolygons)
                    {
                        if (secondFeature.Geometry.Intersection(element).Area / secondFeature.Geometry.Area > 0.1)
                            return false;
                    }
                    if (secondFeature.Geometry.Intersection(largestPolygon).Area / secondFeature.Geometry.Area > 0.9)
                        return true;
                    return false;
                }
            }

            else if (firstFeature.Geometry.Boundary.NumGeometries == 1 && secondFeature.Geometry.Boundary.NumGeometries > 1)
            {
                var polygonsList = new List<Polygon>();
                Polygon largestPolygon = null;

                foreach (var element in (secondFeature.Geometry.Boundary as MultiLineString).Geometries)
                {
                    var newPolygon = Polygon.DefaultFactory.CreatePolygon(element.Coordinates);
                    polygonsList.Add(newPolygon);
                    if (largestPolygon == null || largestPolygon.Area < newPolygon.Area)
                        largestPolygon = newPolygon;
                }

                polygonsList.Remove(largestPolygon);
                var cuttingPolygons = new List<Polygon>();

                foreach (var element in polygonsList)
                {
                    if (largestPolygon.Covers(element))
                        cuttingPolygons.Add(element);
                    else largestPolygon.Union(element);
                }

                if (largestPolygon.Intersects(secondFeature.Geometry))
                {
                    foreach (var element in cuttingPolygons)
                    {
                        if (firstFeature.Geometry.Intersection(element).Area / secondFeature.Geometry.Area > 0.1)
                            return false;
                    }
                    if (firstFeature.Geometry.Intersection(largestPolygon).Area / secondFeature.Geometry.Area > 0.9)
                        return true;
                    else return false;
                }
            }

            return false;
        }
    }
}
