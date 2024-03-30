namespace Gems.AddressRegistry.OsmDataParser.Model;

public class Village : RealObject
{
    public District District { get; set; } = null!;
}