using OsmSharp;

namespace OSM_client;

public class OsmData
{
    private List<Node> Nodes { get; set; }
    private List<Way> Ways { get; set; }
    private List<Relation> Relations { get; set; }

    public OsmData(List<Node> nodes, List<Way> ways, List<Relation> relations)
    {
        Nodes = nodes;
        Ways = ways;
        Relations = relations;
    }
}