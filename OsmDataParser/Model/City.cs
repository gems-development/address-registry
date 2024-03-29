namespace Gems.AddressRegistry.OsmDataParser.Model;

public class City : RealObject
{
    public Settlement Settlement { get; set; } = null!;
}