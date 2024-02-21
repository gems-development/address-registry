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
    private readonly IOsmParser<City> _cityParser;
    private readonly IOsmParser<Street> _streetParser;

    public CityAndStreetGrouper(IOsmParser<City> cityParser, IOsmParser<Street> streetParser)
    {
        _cityParser = cityParser;
        _streetParser = streetParser;
    }

    public bool Group(OsmData osmData, string cityName, string streetName)
    {
        var city = _cityParser.Parse(osmData, cityName, null);
        var cityGeoJson = MultiPolygonSerializer.Serialize(city, osmData);
        
        var street = _streetParser.Parse(osmData, streetName, null);
        var streetGeoJson = MultiLineSerializer.Serialize(street, osmData);
        
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