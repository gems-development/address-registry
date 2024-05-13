using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser;

internal static class OsmParserCore
{
    internal static List<Relation> GetDistrictRelations(OsmData osmData, string areaName)
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

    internal static List<Way> MergeByMatchingId(List<Way> osmObjects)
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