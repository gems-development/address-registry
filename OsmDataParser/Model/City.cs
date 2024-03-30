namespace Gems.AddressRegistry.OsmDataParser.Model;

public class City : RealObject
{
    public District District { get; set; } = null!;
}