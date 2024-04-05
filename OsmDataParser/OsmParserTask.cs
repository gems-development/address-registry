using Gems.AddressRegistry.OsmDataParser.DataGroupingServices;
using Gems.AddressRegistry.OsmDataParser.Factories;
using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataParser;

public static class OsmParserTask
{
    public static async Task<IReadOnlyCollection<House>> Execute(string pathToPbf, string areaName)
    {
        var osmData = await OsmDataReader.Read(pathToPbf);
        
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
        
        return resultHouses;
    }
}