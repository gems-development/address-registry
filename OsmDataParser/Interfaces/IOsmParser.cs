using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Interfaces;

public interface IOsmParser<TResult>
{
    TResult Parse(OsmData osmData, string areaName, ILogger logger, string districtName);
    IReadOnlyCollection<TResult> ParseAll(OsmData osmData, ILogger logger, string areaName);
}