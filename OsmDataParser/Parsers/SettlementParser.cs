using osmDataParser;
using osmDataParser.Interfaces;
using osmDataParser.model;
using OsmDataParser.Support;

namespace OsmDataParser.Parsers;

public class SettlementParser : ISettlementParser
{
    public List<Settlement> GetSettlements(OsmData osmData, string areaName)
    {
        var settlementList = new List<Settlement>();
        var districts = OsmParserCore.GetDistrictRelations(osmData, areaName);

        foreach (var district in districts)
        {
            var districtMemberIds = district.Members.Select(o => o.Id).ToHashSet();
            var settlements = osmData.Relations.Where(rel => districtMemberIds.Contains(rel.Id ?? -1)).ToList();

            foreach (var settlement in settlements)
            {
                var resultSettlement = new Settlement { Name = settlement.Tags[OsmKeywords.Name] };
                var settlementMemberIds = settlement.Members.Select(o => o.Id).ToHashSet();
                var relationWays = osmData.Ways.Where(way => settlementMemberIds.Contains(way.Id ?? -1)).ToList();
                var osmObjects = OsmParserCore.MergeByMatchingId(relationWays);
                
                foreach (var way in osmObjects)
                    resultSettlement.Components.Add(way);
        
                settlementList.Add(resultSettlement);
            }
        }
        return settlementList;
    }
}