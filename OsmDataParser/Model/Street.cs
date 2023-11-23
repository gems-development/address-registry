using OsmSharp;

namespace osmDataParser.model;

public class Street
{
    public string Name { get; init; }
    public ICollection<Way> Components { get; } = new List<Way>();
}