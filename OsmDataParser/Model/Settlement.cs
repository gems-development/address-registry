using OsmSharp;

namespace osmDataParser.model;

public class Settlement
{
    public string Name { get; init; }
    public ICollection<Way> Components { get; } = new List<Way>();
}