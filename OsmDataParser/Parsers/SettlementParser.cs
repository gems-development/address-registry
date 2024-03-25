using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Serializers;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

internal sealed class SettlementParser : IOsmParser<Settlement>
{
    private readonly IOsmToGeoJsonConverter _converter = new MultiPolygonSerializer();
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
                var settlementMemberIds = settlement.Members.Select(o => o.Id).ToHashSet();
                var relationWays = osmData.Ways.Where(way => settlementMemberIds.Contains(way.Id ?? -1)).ToList();
                var components = OsmParserCore.MergeByMatchingId(relationWays);
                
                var cleanedName = ObjectNameCleaner.Clean(settlement.Tags[OsmKeywords.Name]);
                var resultSettlement = new Settlement
                {
                    Name = cleanedName,
                    GeoJson = _converter.Serialize(components, cleanedName, osmData)
                };
        
                settlementList.Add(resultSettlement);
                Console.WriteLine("Объект {" + resultSettlement.Name + "} добавлен в коллекцию поселений.");
            }
        }
        return settlementList;
    }
}