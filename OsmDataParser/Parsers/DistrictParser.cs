using osmDataParser;
using osmDataParser.Interfaces;
using osmDataParser.model;
using OsmDataParser.Support;

namespace OsmDataParser.Parsers;

public class DistrictParser : IDistrictParser
{
    public List<District> GetDistricts(OsmData osmData, string areaName)
    {
        var districtList = new List<District>();
        var districts = OsmParserCore.GetDistrictRelations(osmData, areaName);
        
        foreach (var district in districts)
        {
            var resultDistrict = new District { Name = district.Tags[OsmKeywords.Name] };
            var districtMemberIds = district.Members.Select(o => o.Id).ToHashSet();
            var relationWays = osmData.Ways.Where(way => districtMemberIds.Contains(way.Id ?? -1)).ToList();
            var osmObjects = OsmParserCore.MergeByMatchingId(relationWays);

            foreach (var way in osmObjects)
                resultDistrict.Components.Add(way);
        
            districtList.Add(resultDistrict);
        }
            
        return districtList;
    }
}