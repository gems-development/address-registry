using OsmSharp;
using OsmSharp.Tags;

namespace OsmDataParser;

public class StreetDataParser
{
    // Получаем список всех улиц
    public List<Way> GetStreets(List<Way> ways)
    {
        var streets = new List<Way>();
        foreach (var way in ways)
        {
            if (way.Tags.ContainsKey("highway") && way.Tags.ContainsKey("name"))
                streets.Add(way);
        }

        var streetGroup = streets.GroupBy(p => p.Tags["name"]);
        var streetList = MergeByMatchingId(streetGroup);
        
        return streetList;
    }

    // Склеиваем пути в улицу по совпадающим nodeId
    private List<Way> MergeByMatchingId(IEnumerable<IGrouping<string, Way>> streetGroup)
    {
        var sortedWays = new List<Way>();
        
        foreach (var street in streetGroup)
        {
            var group = street.ToList();

            if (group.Count < 1 || group == null)
                throw new ArgumentException("Empty street!");
        
            if (group.Count == 1)
                sortedWays.Add(group.First());
        
            else if (group.Count > 1)
            {
                var mergedWay = group[0];
    
                for (var i = 1; i < group.Count; i++)
                {
                    var currentWay = group[i];
        
                    if (mergedWay.Nodes.LastOrDefault() == currentWay.Nodes.FirstOrDefault())
                    {
                        mergedWay = MergeWays(mergedWay, currentWay); 
                    }
                    else if (mergedWay.Nodes.FirstOrDefault() == currentWay.Nodes.LastOrDefault())
                    {
                        mergedWay = MergeWays(currentWay, mergedWay); 
                    }
                    else
                    {
                        // to do: Обработка разрывных улиц и допилить алгоритм
                    }
                }
                
                sortedWays.Add(mergedWay);
            }
        }
        
        return sortedWays;
    }
    
    private Way MergeWays(Way way1, Way way2)
    {
        // var tagCollect = new TagsCollection();
        // tagCollect.Add("highway", way1.Tags["highway"]);
        // tagCollect.Add("name", way1.Tags["name"]);
        
        var mergedWay = new Way
        {
            Nodes = way1.Nodes.Concat(way2.Nodes.Skip(1)).ToArray(),
            Tags = way1.Tags
        };

        return mergedWay;
    }
}