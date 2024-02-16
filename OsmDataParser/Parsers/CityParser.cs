using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

public class CityParser : ICityParser
{
    public City GetCity(OsmData osmData, string cityName)
    {
        var resultCity = new City { Name = cityName };
        var relations = osmData.Relations;
        
        foreach (var relation in relations)
        {
            if (relation.Tags.ContainsKey(OsmKeywords.Place)
                && relation.Tags[OsmKeywords.Place] == "city"
                && relation.Tags[OsmKeywords.Name] == cityName)
            {
                var districtMemberIds = relation.Members.Select(o => o.Id).ToHashSet();
                var relationWays = osmData.Ways.Where(way => districtMemberIds.Contains(way.Id ?? -1)).ToList();
                var osmObjects = OsmParserCore.MergeByMatchingId(relationWays);

                foreach (var way in osmObjects)
                    resultCity.Components.Add(way);
            }
        }

        return resultCity;
    }
}