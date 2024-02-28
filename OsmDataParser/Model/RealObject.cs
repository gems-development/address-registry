using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Model;

public class RealObject
{
    public string Name { get; init; } = null!;
    public ICollection<Way> Components { get; set; } = new List<Way>();
}