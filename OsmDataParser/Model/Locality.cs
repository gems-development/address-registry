using OsmSharp;

namespace osmDataParser.model;

public class Locality
{
    public string Name { get; init; }
    public ICollection<Way> Components { get; } = new List<Way>();
}