using Microsoft.Extensions.Logging;

namespace Gems.AddressRegistry.OsmDataParser.Interfaces;

public interface INormalizable
{
    string GetNormalizedAddress(ILogger logger);
}