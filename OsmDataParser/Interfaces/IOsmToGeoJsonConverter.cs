using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Interfaces;

public interface IOsmToGeoJsonConverter
{
    string Serialize(RealObject realObject, OsmData osmData);
}