using osmDataParser.model;
using OsmSharp;
using OsmSharp.Tags;

namespace osmDataParser;

public class OsmDataParser
{
    public List<Locality> GetLocalities(OsmData osmData)
    {
        var relations = osmData.Relations;
        var localities = new List<Locality>();
    
        foreach (var relation in relations)
        {
            if (relation.Tags.ContainsKey("boundary") &&
                relation.Tags["boundary"] == "administrative" &&
                relation.Tags.ContainsKey("admin_level") &&
                relation.Tags["admin_level"] == "8" &&
                relation.Tags["name"] == "Лежанское сельское поселение")
            {
                var relationMemberIds = relation.Members.Select(o => o.Id).ToHashSet();
                var relationWays = osmData.Ways.Where(way => relationMemberIds.Contains(way.Id ?? -1)).ToList();
                // var locality = new Locality
                // {
                //     Name = relation.Tags["name"],
                //     Border = relation
                // };
                // localities.Add(locality);
                var locality = MergeByMatchingIdForLocalities(relationWays);
                locality.Name = relation.Tags["name"];
                localities.Add(locality);
            }
        }

        return localities;
    }
    
    public List<Street> GetStreets(OsmData osmData)
    {
        var ways = osmData.Ways;
        var streets = new List<Way>();
        foreach (var way in ways)
            if (way.Tags.ContainsKey("highway") && way.Tags.ContainsKey("name"))
                streets.Add(way);

        var streetGroup = streets.GroupBy(p => p.Tags["name"]);
        var streetList = new List<Street>();

        foreach (var street in streetGroup)
        {
            var resultStreet = MergeByMatchingIdForStreets(street);
            streetList.Add(resultStreet);
        }

        return streetList;
    }

    private Street MergeByMatchingIdForStreets(IGrouping<string, Way> street)
    {
        var group = street.ToList();
        var resultStreet = new Street { Name = group.First().Tags["name"] };

        if (group.Count < 1 || group is null)
            throw new ArgumentException("Empty street!");

        if (group.Count == 1)
        {
            resultStreet.Components.Add(group.First());
        }

        else if (group.Count > 1)
        {
            while (true)
            {
                var merged = false;

                for (var i = 0; i < group.Count; i++)
                {
                    for (var j = 0; j < group.Count; j++)
                        if (i != j)
                        {
                            if (group[i].Nodes.FirstOrDefault() == group[j].Nodes.LastOrDefault())
                            {
                                // Объединяем два пути
                                var newWay = MergeWaysForStreets(group[j], group[i]);

                                // Удаляем старые пути и добавляем новый
                                group.RemoveAt(i);
                                group.RemoveAt(j - 1);
                                group.Add(newWay);

                                merged = true;
                                break;
                            }

                            if (group[i].Nodes.LastOrDefault() == group[j].Nodes.FirstOrDefault())
                            {
                                // Объединяем два пути
                                var newWay = MergeWaysForStreets(group[i], group[j]);

                                // Удаляем старые пути и добавляем новый
                                group.RemoveAt(i);
                                group.RemoveAt(j - 1);
                                group.Add(newWay);

                                merged = true;
                                break;
                            }
                        }

                    if (merged) break;
                }

                if (!merged) break;
            }

            foreach (var way in group)
                resultStreet.Components.Add(way);
        }

        return resultStreet;
    }
    
    private Locality MergeByMatchingIdForLocalities(List<Way> relationWays)
    {
        if (relationWays.Count < 3)
            throw new ArgumentException("It's not a border");

        while (true)
        {
            var merged = false;

            for (var i = 0; i < relationWays.Count; i++)
            {
                for (var j = 0; j < relationWays.Count; j++)
                    if (i != j)
                    {
                        if (relationWays[i].Nodes.FirstOrDefault() == relationWays[j].Nodes.LastOrDefault())
                        {
                            // Объединяем два пути
                            var newWay = MergeWaysForStreets(relationWays[j], relationWays[i]);

                            // Удаляем старые пути и добавляем новый
                            relationWays.RemoveAt(i);
                            relationWays.RemoveAt(j - 1);
                            relationWays.Add(newWay);

                            merged = true;
                            break;
                        }

                        if (relationWays[i].Nodes.LastOrDefault() == relationWays[j].Nodes.FirstOrDefault())
                        {
                            // Объединяем два пути
                            var newWay = MergeWaysForStreets(relationWays[i], relationWays[j]);

                            // Удаляем старые пути и добавляем новый
                            relationWays.RemoveAt(i);
                            relationWays.RemoveAt(j - 1);
                            relationWays.Add(newWay);

                            merged = true;
                            break;
                        }

                        if (relationWays[i].Nodes.FirstOrDefault() == relationWays[j].Nodes.FirstOrDefault())
                        {
                            // Объединяем два пути
                            var newWay = new Way
                            {
                                Nodes = relationWays[j].Nodes.Reverse().Concat(relationWays[i].Nodes.Skip(1)).ToArray(),
                            };

                            // Удаляем старые пути и добавляем новый
                            relationWays.RemoveAt(i);
                            relationWays.RemoveAt(j - 1);
                            relationWays.Add(newWay);

                            merged = true;
                            break;
                        }
                        
                        if (relationWays[i].Nodes.LastOrDefault() == relationWays[j].Nodes.LastOrDefault())
                        {
                            // Объединяем два пути
                            var newWay = new Way
                            {
                                Nodes = relationWays[i].Nodes.
                                    Concat(relationWays[j].Nodes.Take(relationWays[j].Nodes.Length-1).Reverse()).ToArray(),
                            };

                            // Удаляем старые пути и добавляем новый
                            relationWays.RemoveAt(i);
                            relationWays.RemoveAt(j - 1);
                            relationWays.Add(newWay);

                            merged = true;
                            break;
                        }
                    }

                if (merged) break;
            }

            if (!merged) break;
        }
        
        var resultLocality = new Locality();
        
        foreach (var way in relationWays)
            resultLocality.Components.Add(way);

        return resultLocality;
    }
    
    private Way MergeWaysForStreets(Way way1, Way way2)
    {
        // var tagCollect = new TagsCollection();
        // tagCollect.Add("highway", way1.Tags["highway"]);
        // tagCollect.Add("name", way1.Tags["name"]);

        var mergedWay = new Way
        {
            Nodes = way1.Nodes.Concat(way2.Nodes.Skip(1)).ToArray(),
            //Tags = tagCollect
        };

        return mergedWay;
    }
}