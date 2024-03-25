using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Serializers;
using Gems.AddressRegistry.OsmDataParser.Support;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

internal sealed class DistrictParser : IOsmParser<District>
{
    private readonly IOsmToGeoJsonConverter _converter = new MultiPolygonSerializer();
    public District Parse(OsmData osmData, string areaName, string districtName)
    {
        var resultDistrict = new District();
        var districts = ParseAll(osmData, areaName);
        foreach (var district in districts)
        {
            if (district.Name == districtName)
                resultDistrict = district;
        }

        return resultDistrict;
    }
    
    public IReadOnlyCollection<District> ParseAll(OsmData osmData, string areaName)
    {
        var districtList = new List<District>();
        var districts = OsmParserCore.GetDistrictRelations(osmData, areaName);
        
        foreach (var district in districts)
        {
            var districtMemberIds = district.Members.Select(o => o.Id).ToHashSet();
            var relationWays = osmData.Ways.Where(way => districtMemberIds.Contains(way.Id ?? -1)).ToList();
            var components = OsmParserCore.MergeByMatchingId(relationWays);
            
            var cleanedName = ObjectNameCleaner.Clean(district.Tags[OsmKeywords.Name]);
            var resultDistrict = new District
            {
                Name = cleanedName,
                GeoJson = _converter.Serialize(components, cleanedName, osmData)
            };
        
            districtList.Add(resultDistrict);
            Console.WriteLine("Объект {" + resultDistrict.Name + "} добавлен в коллекцию районов.");
        }
            
        return districtList;
    }
}