using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;
using OsmSharp.Streams;

namespace Gems.AddressRegistry.OsmDataParser;

public static class OsmDataReader
{
    public static async Task<OsmData> Read(string pathToPbf)
    {
        var osmData = new OsmData();
        
        await using var fileStream = new FileInfo(pathToPbf).OpenRead();
        using var osmStreamSource = new PBFOsmStreamSource(fileStream);

        foreach (var element in osmStreamSource)
        {
            if (element.Type is OsmGeoType.Node)
                osmData.Nodes.Add((Node)element);
            else if (element.Type is OsmGeoType.Way)
                osmData.Ways.Add((Way)element);
            else if (element.Type is OsmGeoType.Relation)
                osmData.Relations.Add((Relation)element);
        }

        return osmData;
    }
}