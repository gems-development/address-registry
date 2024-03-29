using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Model;

public class RealObject
{
    public long? Id { get; set; }
    public string Name { get; init; } = null!;
    public string GeoJson { get; set; } = null!;
}