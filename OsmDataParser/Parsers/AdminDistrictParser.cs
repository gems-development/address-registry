using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Serializers;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

public class AdminDistrictParser : IOsmParser<AdminDistrict>
{
    private readonly IOsmToGeoJsonConverter _converter = new MultiPolygonSerializer();
    public AdminDistrict Parse(OsmData osmData, string adminDistrictName, string districtName = null!)
    {
        var resultAdminDistrict = new AdminDistrict();
        var adminDistricts = ParseAll(osmData, default!);
        foreach (var adminDistrict in adminDistricts)
        {
            if (adminDistrict.Name == adminDistrictName)
                resultAdminDistrict = adminDistrict;
        }

        return resultAdminDistrict;
    }

    public IReadOnlyCollection<AdminDistrict> ParseAll(OsmData osmData, string areaName = null!)
    {
        var relations = osmData.Relations;
        var adminDistricts = new List<AdminDistrict>();

        foreach (var relation in relations)
        {
            if (relation.Tags.ContainsKey(OsmKeywords.Boundary)
                && relation.Tags.ContainsKey(OsmKeywords.AdminLevel)
                && relation.Tags.ContainsKey(OsmKeywords.Name)
                && relation.Tags[OsmKeywords.Boundary] == OsmKeywords.Administrative 
                && relation.Tags[OsmKeywords.AdminLevel] == OsmKeywords.Level9)
            {
                var relationMemberIds = relation.Members.Select(o => o.Id).ToHashSet();
                var relationWays = osmData.Ways.Where(way => relationMemberIds.Contains(way.Id ?? -1)).ToList();
                var components = OsmParserCore.MergeByMatchingId(relationWays);
                
                var cleanedName = ObjectNameCleaner.Clean(relation.Tags[OsmKeywords.Name]);
                var adminDistrict = new AdminDistrict
                {
                    Name = cleanedName,
                    GeoJson = _converter.Serialize(components, cleanedName, osmData)
                };
                
                adminDistricts.Add(adminDistrict);
                Console.WriteLine("Объект {" + adminDistrict.Name + "} добавлен " +
                                  "в коллекцию административных округов.");
            }
        }

        return adminDistricts;
    }
}