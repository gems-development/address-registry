using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

public class VillageParser : IOsmParser<Village>
{
    public Village Parse(OsmData osmData, string villageName, string? districtName = null)
    {
        var resultVillage = new Village();
        var villages = ParseAll(osmData, default);
        foreach (var village in villages)
        {
            if (village.Name == villageName)
                resultVillage = village;
        }
        
        return resultVillage;
    }

    public IReadOnlyCollection<Village> ParseAll(OsmData osmData, string? areaName = null)
    {
        var relations = osmData.Relations;
        var ways = osmData.Ways;
        var villages = new List<Village>();
        
        foreach (var way in ways)
        {
            if (way.Tags.ContainsKey(OsmKeywords.Place)
                && way.Tags.ContainsKey(OsmKeywords.Name)
                && (way.Tags[OsmKeywords.Place] == OsmKeywords.Village
                || way.Tags[OsmKeywords.Place] == OsmKeywords.Hamlet))
            {
                var resultTown = new Village
                {
                    Name = ObjectNameCleaner.Clean(way.Tags[OsmKeywords.StreetName]),
                    Components = new List<Way> { way }
                };
                villages.Add(resultTown);
                Console.WriteLine("Объект {" + resultTown.Name + "} добавлен в коллекцию сёл.");
            }
        }

        foreach (var relation in relations)
        {
            if (relation.Tags.ContainsKey(OsmKeywords.Place)
                && relation.Tags.ContainsKey(OsmKeywords.Name)
                && (relation.Tags[OsmKeywords.Place] == OsmKeywords.Village
                || relation.Tags[OsmKeywords.Place] == OsmKeywords.Hamlet))
            {
                var resultVillage = new Village { Name = ObjectNameCleaner.Clean(relation.Tags[OsmKeywords.Name]) };
                var districtMemberIds = relation.Members.Select(o => o.Id).ToHashSet();
                var relationWays = osmData.Ways.Where(way => districtMemberIds.Contains(way.Id ?? -1)).ToList();
                resultVillage.Components = OsmParserCore.MergeByMatchingId(relationWays);
                
                villages.Add(resultVillage);
                Console.WriteLine("Объект {" + resultVillage.Name + "} добавлен в коллекцию сёл.");
            }
        }

        return villages;
    }
}