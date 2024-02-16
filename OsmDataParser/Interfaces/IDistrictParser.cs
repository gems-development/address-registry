using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Interfaces;

public interface IDistrictParser
{
    List<District> GetDistricts(OsmData osmData, string areaName);
    District GetDistrict(OsmData osmData, string areaName, string districtName);
}