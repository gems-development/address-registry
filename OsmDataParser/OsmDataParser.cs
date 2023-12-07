using osmDataParser.model;
using OsmSharp;

namespace osmDataParser;

public class OsmDataParser
{
    public List<District> GetDistricts(OsmData osmData, string areaName)
    {
        var districtList = new List<District>();
        var districts = GetDistrictRelations(osmData, areaName);
        
        foreach (var district in districts)
        {
            var resultDistrict = new District { Name = district.Tags[OsmKeywords.Name] };
            var districtMemberIds = district.Members.Select(o => o.Id).ToHashSet();
            var relationWays = osmData.Ways.Where(way => districtMemberIds.Contains(way.Id ?? -1)).ToList();
            var osmObjects = MergeByMatchingId(relationWays);

            foreach (var way in osmObjects)
                resultDistrict.Components.Add(way);
        
            districtList.Add(resultDistrict);
        }
            
        return districtList;
    }

    public List<Settlement> GetSettlements(OsmData osmData, string areaName)
    {
        var settlementList = new List<Settlement>();
        var districts = GetDistrictRelations(osmData, areaName);

        foreach (var district in districts)
        {
            var districtMemberIds = district.Members.Select(o => o.Id).ToHashSet();
            var settlements = osmData.Relations.Where(rel => districtMemberIds.Contains(rel.Id ?? -1)).ToList();

            foreach (var settlement in settlements)
            {
                var resultSettlement = new Settlement { Name = settlement.Tags[OsmKeywords.Name] };
                var settlementMemberIds = settlement.Members.Select(o => o.Id).ToHashSet();
                var relationWays = osmData.Ways.Where(way => settlementMemberIds.Contains(way.Id ?? -1)).ToList();
                var osmObjects = MergeByMatchingId(relationWays);
                
                foreach (var way in osmObjects)
                    resultSettlement.Components.Add(way);
        
                settlementList.Add(resultSettlement);
            }
        }
        return settlementList;
    }

    public List<Street> GetStreets(OsmData osmData)
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
                var osmObjects = MergeByMatchingId(group);

                foreach (var way in osmObjects)
                    resultStreet.Components.Add(way);
            }
            streetList.Add(resultStreet);
        }
        return streetList;
    }

    private List<Relation> GetDistrictRelations(OsmData osmData, string areaName)
    {
        var relations = osmData.Relations;
        var districts = new List<Relation>();

        foreach (var relation in relations)
        {
            if (relation.Tags.ContainsKey(OsmKeywords.Boundary) &&
                relation.Tags[OsmKeywords.Boundary] == OsmKeywords.Administrative &&
                relation.Tags.ContainsKey(OsmKeywords.AdminLevel) &&
                relation.Tags[OsmKeywords.AdminLevel] == OsmKeywords.Level4 &&
                relation.Tags.ContainsKey(OsmKeywords.Name) &&
                relation.Tags[OsmKeywords.Name] == areaName)
            {
                var areaMemberIds = relation.Members.Select(o => o.Id).ToHashSet();
                districts = osmData.Relations.Where(rel => areaMemberIds.Contains(rel.Id ?? -1)).ToList();
            }
        }
        return districts;
    }

    private List<Way> MergeByMatchingId(List<Way> osmObjects)
    {
        while (true)
        {
            var merged = false;

            for (var i = 0; i < osmObjects.Count; i++)
            {
                for (var j = 0; j < osmObjects.Count; j++)
                    if (i != j)
                    {
                        if (osmObjects[i].Nodes.FirstOrDefault() == osmObjects[j].Nodes.LastOrDefault())
                        {
                            // Объединяем два пути
                            var newWay = new Way
                            {
                                Nodes = osmObjects[j].Nodes.Concat(osmObjects[i].Nodes.Skip(1)).ToArray()
                            };

                            // Удаляем старые пути и добавляем новый
                            osmObjects.RemoveAt(i);
                            osmObjects.RemoveAt(j - 1);
                            osmObjects.Add(newWay);

                            merged = true;
                            break;
                        }

                        if (osmObjects[i].Nodes.LastOrDefault() == osmObjects[j].Nodes.FirstOrDefault())
                        {
                            var newWay = new Way
                            {
                                Nodes = osmObjects[i].Nodes.Concat(osmObjects[j].Nodes.Skip(1)).ToArray()
                            };
                            
                            osmObjects.RemoveAt(i);
                            osmObjects.RemoveAt(j - 1);
                            osmObjects.Add(newWay);

                            merged = true;
                            break;
                        }
                        
                        if (osmObjects[i].Nodes.FirstOrDefault() == osmObjects[j].Nodes.FirstOrDefault())
                        {
                            var newWay = new Way
                            {
                                Nodes = osmObjects[j].Nodes.Reverse().Concat(osmObjects[i].Nodes.Skip(1)).ToArray(),
                            };
                            
                            osmObjects.RemoveAt(i);
                            osmObjects.RemoveAt(j - 1);
                            osmObjects.Add(newWay);

                            merged = true;
                            break;
                        }
                    
                        if (osmObjects[i].Nodes.LastOrDefault() == osmObjects[j].Nodes.LastOrDefault())
                        {
                            var newWay = new Way
                            {
                                Nodes = osmObjects[i].Nodes.
                                    Concat(osmObjects[j].Nodes.Take(osmObjects[j].Nodes.Length - 1).Reverse()).ToArray(),
                            };
                            
                            osmObjects.RemoveAt(i);
                            osmObjects.RemoveAt(j - 1);
                            osmObjects.Add(newWay);

                            merged = true;
                            break;
                        }
                    }
                if (merged) break;
            }
            if (!merged) break;
        }
        return osmObjects;
    }
}