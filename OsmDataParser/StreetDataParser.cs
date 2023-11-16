﻿using osmDataParser.model;
using OsmSharp;
using OsmSharp.Tags;

namespace osmDataParser;

public class StreetDataParser
{
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
            var resultStreet = MergeByMatchingId(street);
            streetList.Add(resultStreet);
        }

        return streetList;
    }

    private Street MergeByMatchingId(IGrouping<string, Way> street)
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
                                var newWay = MergeWays(group[j], group[i]);

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
                                var newWay = MergeWays(group[i], group[j]);

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

    private Way MergeWays(Way way1, Way way2)
    {
        var tagCollect = new TagsCollection();
        tagCollect.Add("highway", way1.Tags["highway"]);
        tagCollect.Add("name", way1.Tags["name"]);

        var mergedWay = new Way
        {
            Nodes = way1.Nodes.Concat(way2.Nodes.Skip(1)).ToArray(),
            Tags = tagCollect
        };

        return mergedWay;
    }
}