using Gems.AddressRegistry.OsmDataGroupingService.Serializers;
using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Gems.AddressRegistry.OsmDataGroupingService;

public class CityAndStreetGrouper
{
    public bool Group(string cityGeoJson, string streetGeoJson)
    {
        var reader = new GeoJsonReader();
        var cityGeometry = reader.Read<FeatureCollection>(cityGeoJson);
        var streetGeometry = reader.Read<FeatureCollection>(streetGeoJson);

        var firstFeature = cityGeometry.FirstOrDefault();
        var secondFeature = streetGeometry.FirstOrDefault();
        
        if (firstFeature?.Geometry is MultiPolygon multiPolygon
            && secondFeature?.Geometry is MultiLineString multiLineString)
        {
            return multiPolygon.Intersects(multiLineString);
        }
        
        return false;
    }
}