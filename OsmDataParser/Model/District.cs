using OsmSharp;

namespace osmDataParser.model;

public class District
{
    public string Name { get; init; }
    public ICollection<Way> Components { get; } = new List<Way>();
}