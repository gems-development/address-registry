using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Interfaces;

public interface IOsmToGeoJsonConverter
{
    string Serialize(List<Way> components, string name, OsmData osmData);
}