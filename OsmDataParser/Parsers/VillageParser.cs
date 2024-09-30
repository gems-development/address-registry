using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.OsmDataParser.Factories;
using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

public class VillageParser : IOsmParser<Village>
{
    public Village Parse(OsmData osmData, string villageName, ILogger logger, string? districtName = null)
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

    public IReadOnlyCollection<Village> ParseAll(OsmData osmData, ILogger logger, string? areaName = null)
    {
        var relations = osmData.Relations;
        var ways = osmData.Ways;
        var villages = new List<Village>();
        logger.LogDebug("OSM || Начат анализ сел");

        foreach (var way in ways)
        {
            if (way.Tags.ContainsKey(OsmKeywords.Place)
                && way.Tags.ContainsKey(OsmKeywords.Name)
                && (way.Tags[OsmKeywords.Place] == OsmKeywords.Village
                || way.Tags[OsmKeywords.Place] == OsmKeywords.Hamlet))
            {
                var converter = OsmToGeoJsonConverterFactory.Create<Village>();
                var cleanedName = ObjectNameCleaner.Clean(way.Tags[OsmKeywords.Name]);
                var resultTown = new Village
                {
                    Id = way.Id,
                    Name = cleanedName,
                    GeoJson = converter.Serialize(new List<Way> { way }, cleanedName, osmData)
                };
                villages.Add(resultTown);
                logger.LogTrace("Объект {" + resultTown.Name + "} добавлен в коллекцию сёл.");
            }
        }

        foreach (var relation in relations)
        {
            if (relation.Tags.ContainsKey(OsmKeywords.Place)
                && relation.Tags.ContainsKey(OsmKeywords.Name)
                && (relation.Tags[OsmKeywords.Place] == OsmKeywords.Village
                || relation.Tags[OsmKeywords.Place] == OsmKeywords.Hamlet))
            {
                var districtMemberIds = relation.Members.Select(o => o.Id).ToHashSet();
                var relationWays = osmData.Ways.Where(way => districtMemberIds.Contains(way.Id ?? -1)).ToList();
                var components = OsmParserCore.MergeByMatchingId(relationWays);
                
                var converter = OsmToGeoJsonConverterFactory.Create<Village>();
                var cleanedName = ObjectNameCleaner.Clean(relation.Tags[OsmKeywords.Name]);
                var resultVillage = new Village
                {
                    Id = relation.Id,
                    Name = cleanedName,
                    GeoJson = converter.Serialize(components, cleanedName, osmData)
                };
                
                villages.Add(resultVillage);
                logger.LogTrace("Объект {" + resultVillage.Name + "} добавлен в коллекцию сёл.");
            }
        }
        logger.LogDebug("OSM || Завершен анализ сел");

        return villages;
    }
}