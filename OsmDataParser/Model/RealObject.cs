using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Model;

public class RealObject
{
    public string Name { get; init; } = null!;
    public string GeoJson { get; set; } = null!;
}