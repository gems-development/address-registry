using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

public class AreaParser : IOsmParser<Area>
{
    public Area Parse(OsmData osmData, string areaName, string districtName = null!)
    {
        var relations = osmData.Relations;
        var resultArea = new Area { Name = ObjectNameCleaner.Clean(areaName) };
        
        foreach (var relation in relations)
        {
            if (relation.Tags.ContainsKey(OsmKeywords.Boundary) &&
                relation.Tags[OsmKeywords.Boundary] == OsmKeywords.Administrative &&
                relation.Tags.ContainsKey(OsmKeywords.AdminLevel) &&
                relation.Tags[OsmKeywords.AdminLevel] == OsmKeywords.Level4 &&
                relation.Tags.ContainsKey(OsmKeywords.Name) &&
                relation.Tags[OsmKeywords.Name] == areaName)
            {
                var areaMemberIds = relation.Members.Select(o => o.Id).ToHashSet();
                var areaWays = osmData.Ways.Where(rel => areaMemberIds.Contains(rel.Id ?? -1)).ToList();
                resultArea.Components = OsmParserCore.MergeByMatchingId(areaWays);
            }
        }
        Console.WriteLine("Объект {" + resultArea.Name + "} успешно получена.");

        return resultArea;
    }

    public IReadOnlyCollection<Area> ParseAll(OsmData osmData, string areaName = null!)
    {
        throw new NotImplementedException();
    }
}