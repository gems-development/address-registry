using OsmSharp;
using OsmSharp.Tags;

namespace osmDataParser.Tests;

public static class TestHelper
{
    public static Node CreateNode(long id)
    {
        return new Node { Id = id };
    }

    public static Way CreateWay(long id, TagsCollection tags, long[] nodeIds)
    {
        return new Way { Id = id, Nodes = nodeIds, Tags = tags };
    }

    public static Relation CreateRelation(long id, TagsCollection tags, RelationMember[] members)
    {
        return new Relation { Id = id, Members = members, Tags = tags };
    }
}