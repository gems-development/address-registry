using OsmSharp;

namespace OsmDataParser;

public class OsmData
{
    public List<Node> Nodes = new List<Node>();
    public List<Way> Ways = new List<Way>();
    public List<Relation> Relations = new List<Relation>();
}