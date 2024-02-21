using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

internal sealed class StreetParser : IOsmParser<Street>
{
    public Street Parse(OsmData osmData, string streetName, string? districtName = null)
    {
        var resultStreet = new Street();
        var streets = ParseAll(osmData);
        foreach (var street in streets)
        {
            if (street.Name == streetName)
                resultStreet = street;
        }

        return resultStreet;
    }
    
    public IReadOnlyCollection<Street> ParseAll(OsmData osmData, string? areaName = null)
    {
        var ways = osmData.Ways;
        var streets = new List<Way>();
        
        foreach (var way in ways)
        {
            if (way.Tags.ContainsKey(OsmKeywords.Highway) && way.Tags.ContainsKey(OsmKeywords.Name))
                streets.Add(way);
        }

        var streetGroup = streets.GroupBy(p => p.Tags[OsmKeywords.Name]);
        var streetList = new List<Street>();

        foreach (var street in streetGroup)
        {
            var group = street.ToList();
            var resultStreet = new Street { Name = group.First().Tags[OsmKeywords.Name] };
            
            if (group.Count < 1 || group is null)
                throw new ArgumentException("Empty street");
            
            if (group.Count == 1)
                resultStreet.Components.Add(group.First());
            
            else if (group.Count > 1)
            {
                var osmObjects = OsmParserCore.MergeByMatchingId(group);

                foreach (var way in osmObjects)
                    resultStreet.Components.Add(way);
            }
            streetList.Add(resultStreet);
        }
        return streetList;
    }
}