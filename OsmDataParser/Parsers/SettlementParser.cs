using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

internal sealed class SettlementParser : IOsmParser<Settlement>
{
    public Settlement Parse(OsmData osmData, string areaName, string settlementName)
    {
        var resultSettlement = new Settlement();
        var settlements = ParseAll(osmData, areaName);
        foreach (var settlement in settlements)
        {
            if (settlement.Name == settlementName)
                resultSettlement = settlement;
        }

        return resultSettlement;
    }
    
    public IReadOnlyCollection<Settlement> ParseAll(OsmData osmData, string areaName)
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
                resultSettlement.Components = OsmParserCore.MergeByMatchingId(relationWays);
        
                settlementList.Add(resultSettlement);
                Console.WriteLine("Объект {" + resultSettlement.Name + "} добавлен в коллекцию поселений.");
            }
        }
        return settlementList;
    }
}