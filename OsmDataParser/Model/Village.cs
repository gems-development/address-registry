namespace Gems.AddressRegistry.OsmDataParser.Model;

public class Village : RealObject
{
    public Settlement Settlement { get; set; } = null!;
}