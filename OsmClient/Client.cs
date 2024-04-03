using System.Diagnostics;
using Gems.AddressRegistry.OsmDataGroupingService;
using Gems.AddressRegistry.OsmDataParser.Factories;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using Microsoft.Extensions.Configuration;
using OsmSharp;
using OsmSharp.Streams;

namespace Gems.AddressRegistry.OsmClient;

public static class Client
{
    private static readonly IConfiguration Configuration;
    
    static Client()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;

        Configuration = new ConfigurationBuilder()
            .SetBasePath(projectDirectory!)
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public static async Task Main(string[] args)
    {
        var sw = Stopwatch.StartNew();
        var areaName = Configuration.GetSection("Target Area").Value;
        var osmData = await GetSortedOsmData();
        
        var areaParser = OsmParserFactory.Create<Area>();
        var districtParser = OsmParserFactory.Create<District>();
        var cityParser = OsmParserFactory.Create<City>();
        var villageParser = OsmParserFactory.Create<Village>();
        var streetParser = OsmParserFactory.Create<Street>();
        var houseParser = OsmParserFactory.Create<House>();
        
        var area = areaParser.Parse(osmData, areaName!, default!);
        var districts = districtParser.ParseAll(osmData, areaName!);
        var cities = cityParser.ParseAll(osmData, default!);
        var villages = villageParser.ParseAll(osmData, default!);
        var streets = streetParser.ParseAll(osmData, default!);
        var houses = houseParser.ParseAll(osmData, default!);
        
        ObjectLinkBuilder.LinkAddressElements(area, districts, cities, villages, streets, houses);

        var resultHouses = UnusedAddressesCleaner.Clean(houses);
        
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }

    private static async Task<OsmData> GetSortedOsmData()
    {
        var osmData = new OsmData();
        var pathToPbf = Configuration.GetSection("Pbf File").Value;
        
        await using var fileStream = new FileInfo(pathToPbf!).OpenRead();
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