using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Interfaces;

public interface IOsmParser<TResult>
{
    TResult Parse(OsmData osmData, string areaName, string districtName);
    IReadOnlyCollection<TResult> ParseAll(OsmData osmData, string areaName);
}