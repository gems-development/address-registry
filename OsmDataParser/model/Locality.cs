using OsmSharp;

namespace osmDataParser.model;

public class Locality
{
    public string Name { get; set; }
    public ICollection<Way> Components { get; } = new List<Way>();
}