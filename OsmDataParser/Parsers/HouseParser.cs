using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.OsmDataParser.Factories;
using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

public class HouseParser : IOsmParser<House>
{
    public House Parse(OsmData osmData, string streetName, ILogger logger, string houseNumber)
    {
        var resultHouse = new House();
        var houses = ParseAll(osmData, logger, streetName);
        foreach (var house in houses)
        {
            if (house.Name == streetName && house.Number == houseNumber)
                resultHouse = house;
        }
        
        return resultHouse;
    }

    public IReadOnlyCollection<House> ParseAll(OsmData osmData, ILogger logger, string streetName = null!)
    {
        var ways = osmData.Ways;
        var housesList = new List<House>();
        logger.LogDebug("OSM || Начат анализ домов");

        foreach (var way in ways)
        {
            if (way.Tags.ContainsKey(OsmKeywords.Building) && 
                way.Tags.ContainsKey(OsmKeywords.StreetName) &&
                way.Tags.ContainsKey(OsmKeywords.HouseNumber))
            {
                var converter = OsmToGeoJsonConverterFactory.Create<House>();
                var cleanedName = ObjectNameCleaner.Clean(way.Tags[OsmKeywords.StreetName]);
                var resultHouse = new House
                {
                    Id = way.Id,
                    Name = cleanedName,
                    Number = way.Tags[OsmKeywords.HouseNumber],
                    GeoJson = converter.Serialize(new List<Way> { way }, cleanedName, osmData)
                };
                housesList.Add(resultHouse);
                logger.LogTrace($"Объект с адресом {resultHouse.Name}, " +
                                $"д. {resultHouse.Number} добавлен в коллекцию домов.");
            }
        }
        logger.LogDebug("OSM || Завершен анализ домов");

        return housesList;
    }
}