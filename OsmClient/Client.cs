using OsmSharp;
using OsmSharp.Streams;

namespace OSM_client;

public class Client
{
    private const string Path = "osm_data.xml";

    public static void Main(string[] args)
    {
    }
        
    public async Task<OsmData> SortData()
    {
        var nodes = new List<Node>();
        var ways = new List<Way>();
        var relations = new List<Relation>();
            
        var xmlData = await GetData();
        await File.WriteAllTextAsync(Path, xmlData);
        var fileStream = new FileInfo(Path).OpenRead();
        var osmStreamSource = new XmlOsmStreamSource(fileStream);
            
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

        var osmData = new OsmData(nodes, ways, relations);
        return osmData;
    }
        
    private Task<string> GetData()
    {
        const string url = "https://overpass-api.de/api/interpreter";
        var overpassClient = new OverpassApiClient(url);

        // Запрос к Overpass API
        const string query = "[out:xml];" +
                             "\nnode(54.979788, 73.414227, 54.983705, 73.423204);" +
                             "\nout body;" +
                             "\nway(54.979788, 73.414227, 54.983705, 73.423204);" +
                             "\nout body;" +
                             "\nrelation(54.979788, 73.414227, 54.983705, 73.423204);" +
                             "\nout body;";
        
            
        return overpassClient.ExecuteOverpassQueryAsync(query);
    }
}