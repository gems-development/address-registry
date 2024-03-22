namespace Gems.AddressRegistry.OsmDataParser.Model;

public class House : RealObject
{
    public string Number { get; init; } = null!;
    public Street Street { get; set; } = null!;
}