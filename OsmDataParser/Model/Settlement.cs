namespace Gems.AddressRegistry.OsmDataParser.Model;

public class Settlement : RealObject
{
    public long? DistrictId { get; set; }
    public District District { get; set; } = null!;
}