using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

internal sealed class CityParser : IOsmParser<City>
{
    public City Parse(OsmData osmData, string cityName, string? districtName = null)
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
    
    public IReadOnlyCollection<City> ParseAll(OsmData osmData, string? areaName = null)
    {
        var relations = osmData.Relations;
        var cities = new List<City>();
        
        foreach (var relation in relations)
        {
            if (relation.Tags.ContainsKey(OsmKeywords.Place)
                && relation.Tags.ContainsKey(OsmKeywords.Name)
                && relation.Tags[OsmKeywords.Place] == "city")
            {
                var resultCity = new City { Name = relation.Tags[OsmKeywords.Name] };
                var districtMemberIds = relation.Members.Select(o => o.Id).ToHashSet();
                var relationWays = osmData.Ways.Where(way => districtMemberIds.Contains(way.Id ?? -1)).ToList();
                var osmObjects = OsmParserCore.MergeByMatchingId(relationWays);

                foreach (var way in osmObjects)
                    resultCity.Components.Add(way);
                
                cities.Add(resultCity);
                Console.WriteLine("Объект {" + resultCity.Name + "} добавлен в коллекцию городов.");
            }
        }

        return cities;
    }
}