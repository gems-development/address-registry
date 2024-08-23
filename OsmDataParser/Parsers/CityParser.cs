using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.OsmDataParser.Factories;
using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

internal sealed class CityParser : IOsmParser<City>
{
    public City Parse(OsmData osmData, string cityName, ILogger logger, string? districtName = null)
    {
        var resultCity = new City();
        var cities = ParseAll(osmData, default);
        foreach (var city in cities)
        {
            if (city.Name == cityName)
                resultCity = city;
        }

        return resultCity;
    }
    
    public IReadOnlyCollection<City> ParseAll(OsmData osmData, ILogger logger, string? areaName = null)
    {
        var relations = osmData.Relations;
        var ways = osmData.Ways;
        var cities = new List<City>();
        logger.LogDebug("OSM || Начат анализ городов");
        foreach (var way in ways)
        {
            if (way.Tags.ContainsKey(OsmKeywords.Place)
                && way.Tags.ContainsKey(OsmKeywords.Name)
                && way.Tags[OsmKeywords.Place] == OsmKeywords.Town)
            {
                var converter = OsmToGeoJsonConverterFactory.Create<City>();
                var cleanedName = ObjectNameCleaner.Clean(way.Tags[OsmKeywords.Name]);
                var resultTown = new City
                {
                    Id = way.Id,
                    Name = cleanedName,
                    GeoJson = converter.Serialize(new List<Way> { way }, cleanedName, osmData)
                };
                cities.Add(resultTown);
                logger.LogTrace("Объект {" + resultTown.Name + "} добавлен в коллекцию городов.");
            }
        }
        
        foreach (var relation in relations)
        {
            if (relation.Tags.ContainsKey(OsmKeywords.Place)
                && relation.Tags.ContainsKey(OsmKeywords.Name)
                && (relation.Tags[OsmKeywords.Place] == OsmKeywords.City
                || relation.Tags[OsmKeywords.Place] == OsmKeywords.Town))
            {
                var districtMemberIds = relation.Members.Select(o => o.Id).ToHashSet();
                var relationWays = osmData.Ways.Where(way => districtMemberIds.Contains(way.Id ?? -1)).ToList();
                var components = OsmParserCore.MergeByMatchingId(relationWays);
                
                var converter = OsmToGeoJsonConverterFactory.Create<City>();
                var cleanedName = ObjectNameCleaner.Clean(relation.Tags[OsmKeywords.Name]);
                var resultCity = new City
                {
                    Id = relation.Id,
                    Name = cleanedName,
                    GeoJson = converter.Serialize(components, cleanedName, osmData)
                };
                
                cities.Add(resultCity);
                logger.LogTrace("Объект {" + resultCity.Name + "} добавлен в коллекцию городов.");
            }
        }
        logger.LogDebug("OSM || Завершен анализ городов");

        return cities;
    }
}