using Gems.AddressRegistry.OsmDataGroupingService;
using Gems.AddressRegistry.OsmDataParser.Factories;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;
using OsmSharp.Streams;

namespace Gems.AddressRegistry.OsmClient;

public static class Client
{
    private const string PathToPbf = "RU-YEV.osm.pbf";
    private const string OutputFilePath = "data.geojson";
    private const string AreaName = "Еврейская автономная область";

    public static async Task Main(string[] args)
    {
        var osmData = await GetSortedOsmData();
        
        var areaParser = OsmParserFactory.Create<Area>();
        var districtParser = OsmParserFactory.Create<District>();
        var settlementParser = OsmParserFactory.Create<Settlement>();
        var cityParser = OsmParserFactory.Create<City>();
        var villageParser = OsmParserFactory.Create<Village>();
        var streetParser = OsmParserFactory.Create<Street>();
        var houseParser = OsmParserFactory.Create<House>();
        
        var area = areaParser.Parse(osmData, AreaName, default!);
        var districts = districtParser.ParseAll(osmData, AreaName);
        var settlements = settlementParser.ParseAll(osmData, AreaName);
        var cities = cityParser.ParseAll(osmData, default!);
        var villages = villageParser.ParseAll(osmData, default!);
        var streets = streetParser.ParseAll(osmData, default!);
        var houses = houseParser.ParseAll(osmData, default!);
        
        ObjectLinkBuilder.LinkAddressElements(area, districts, settlements, cities, villages, streets, houses);
        
        await File.WriteAllTextAsync(OutputFilePath, area.GeoJson);
    }

    private static async Task<OsmData> GetSortedOsmData()
    {
        var osmData = new OsmData();
        await using var fileStream = new FileInfo(PathToPbf).OpenRead();
        using var osmStreamSource = new PBFOsmStreamSource(fileStream);
        
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