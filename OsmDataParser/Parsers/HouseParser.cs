using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Serializers;
using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

public class HouseParser : IOsmParser<House>
{
    private readonly IOsmToGeoJsonConverter _converter = new MultiPolygonSerializer();
    public House Parse(OsmData osmData, string streetName, string houseNumber)
    {
        var resultHouse = new House();
        var houses = ParseAll(osmData, streetName);
        foreach (var house in houses)
        {
            if (house.Name == streetName && house.Number == houseNumber)
                resultHouse = house;
        }
        
        return resultHouse;
    }

    public IReadOnlyCollection<House> ParseAll(OsmData osmData, string streetName)
    {
        var ways = osmData.Ways;
        var housesList = new List<House>();
        
        foreach (var way in ways)
        {
            if (way.Tags.ContainsKey(OsmKeywords.Building) && 
                way.Tags.ContainsKey(OsmKeywords.StreetName) &&
                way.Tags.ContainsKey(OsmKeywords.HouseNumber) &&
                way.Tags[OsmKeywords.StreetName] == streetName)
            {
                var cleanedName = ObjectNameCleaner.Clean(way.Tags[OsmKeywords.StreetName]);
                var resultHouse = new House
                {
                    Name = cleanedName,
                    Number = way.Tags[OsmKeywords.HouseNumber],
                    GeoJson = _converter.Serialize(new List<Way> { way }, cleanedName, osmData)
                };
                housesList.Add(resultHouse);
                Console.WriteLine($"Объект с адресом {resultHouse.Name}, " +
                                  $"д. {resultHouse.Number} добавлен в коллекцию домов.");
            }
        }

        return housesList;
    }
}