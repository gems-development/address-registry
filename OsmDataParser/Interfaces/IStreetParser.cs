using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Interfaces;

public interface IStreetParser
{
    Street GetStreet(OsmData osmData, string street);
    List<Street> GetStreets(OsmData osmData);
}