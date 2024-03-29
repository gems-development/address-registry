using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Serializers;

namespace Gems.AddressRegistry.OsmDataParser.Factories;

public static class OsmToGeoJsonConverterFactory
{
    public static IOsmToGeoJsonConverter Create<TData>()
    {
        if (typeof(TData) == typeof(Street))
            return new MultiLineSerializer();
        
        return new MultiPolygonSerializer();
    }
}