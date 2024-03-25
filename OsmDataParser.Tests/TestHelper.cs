using OsmSharp;
using OsmSharp.Tags;

namespace Gems.AddressRegistry.OsmDataParser.Tests;

internal static class TestHelper
{
    public static Node CreateNode(long id, double lat, double lon)
    {
        return new Node
        {
            Id = id,
            Latitude = lat,
            Longitude = lon
        };
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