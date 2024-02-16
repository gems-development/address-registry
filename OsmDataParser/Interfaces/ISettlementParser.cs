using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Interfaces;

public interface ISettlementParser
{
    List<Settlement> GetSettlements(OsmData osmData, string areaName);
}