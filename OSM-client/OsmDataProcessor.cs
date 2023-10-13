using OsmSharp;
using OsmSharp.Streams;

namespace OSM_client;

public class OsmDataProcessor
{
    public void ProcessOsmData(string path)
    {
        var fileStream = new FileInfo(path).OpenRead();
        var osmStreamSource = new XmlOsmStreamSource(fileStream);
        
        var nodes = new List<Node>();
        var ways = new List<Way>();
        var relations = new List<Relation>();
        
        // сортируем объекты OSM-модели по категориям
        foreach (var element in osmStreamSource)
        {
            if (element.Type is OsmGeoType.Node)
                nodes.Add((Node) element);
            
            else if (element.Type is OsmGeoType.Way)
                ways.Add((Way) element);
            
            else if (element.Type is OsmGeoType.Relation)
                relations.Add((Relation) element);
        }

        // Выводим геометрию улиц
        var streets = GetStreetList(ways);
        foreach (var street in streets)
        {
            Console.WriteLine($"\nГеометрия улицы \"{street.Tags["name"]}\":");
            foreach (var nodeId in street.Nodes)
            {
                foreach (var node in nodes.Where(node => node.Id == nodeId))
                {
                    Console.WriteLine("Координаты: Lat = " + node.Latitude + ", Lon = " + node.Longitude);
                }
            }
        }
    }

    // Получаем список всех улиц
    private List<Way> GetStreetList(List<Way> ways)
    {
        var streets = new List<Way>();
        foreach (var way in ways)
        {
            if (way.Tags.ContainsKey("highway") && way.Tags["highway"] == "residential")
            {
                if (way.Tags.ContainsKey("name"))
                    streets.Add(way);
            }   
        }

        return streets;
    }
}