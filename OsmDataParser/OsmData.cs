using OsmSharp;

namespace osmDataParser;

public class OsmData
{
    public ICollection<Node> Nodes { get; } = new List<Node>();
    public ICollection<Way> Ways { get; } = new List<Way>();
    public ICollection<Relation> Relations { get; } = new List<Relation>();
}