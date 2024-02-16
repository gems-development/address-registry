using osmDataParser.Interfaces;
using OsmDataParser.Parsers;
using OsmDataParser.Support;
using OsmSharp;
using OsmSharp.Streams;

namespace osm_client;

public class Client
{
    private const string PathToPbf = "RU-YEV.osm.pbf";
    private const string OutputFilePath = "data.geojson";

    public static async Task Main(string[] args)
    {
        var osmData = await GetSortedOsmData();
        IDistrictParser districtParser = new DistrictParser();
        var districts = districtParser.GetDistricts(osmData, "Еврейская автономная область");
        var osmDataSerializer = new OsmDataSerializer();

        foreach (var district in districts)
        {
            var geoJson = osmDataSerializer.SerializeDistrict(district, osmData);
            await File.WriteAllTextAsync(OutputFilePath, geoJson);
            Console.WriteLine("Граница {" + district.Name + "} записана в формате GeoJSON.");
        }
    }

    private static async Task<OsmData> GetSortedOsmData()
    {
        var osmData = new OsmData();
        await using var fileStream = new FileInfo(PathToPbf).OpenRead();
        using var osmStreamSource = new PBFOsmStreamSource(fileStream);

        // сортируем объекты OSM-модели по категориям
        foreach (var element in osmStreamSource)
            if (element.Type is OsmGeoType.Node)
                osmData.Nodes.Add((Node)element);

            else if (element.Type is OsmGeoType.Way)
                osmData.Ways.Add((Way)element);

            else if (element.Type is OsmGeoType.Relation)
                osmData.Relations.Add((Relation)element);

        return osmData;
    }
}