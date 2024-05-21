using Gems.AddressRegistry.OsmDataParser.DataGroupingServices;
using Gems.AddressRegistry.OsmDataParser.Factories;
using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataParser;

public static class OsmParserTask
{
	public static async Task<IReadOnlyCollection<House>> Execute(string pathToPbf, string areaName)
	{
		var osmData = await OsmDataReader.Read(pathToPbf);
		var area = OsmParserFactory.Create<Area>().Parse(osmData, areaName, default!);
		IReadOnlyCollection<District>? districts = null;
		IReadOnlyCollection<City>? cities = null;
		IReadOnlyCollection<Village>? villages = null;
		IReadOnlyCollection<Street>? streets = null;
		IReadOnlyCollection<House>? houses = null;

		await Task.WhenAll(
			Task.Run(() => districts = OsmParserFactory.Create<District>().ParseAll(osmData, string.Empty)),
			Task.Run(() => cities = OsmParserFactory.Create<City>().ParseAll(osmData, string.Empty)),
			Task.Run(() => villages = OsmParserFactory.Create<Village>().ParseAll(osmData, string.Empty)),
			Task.Run(() => streets = OsmParserFactory.Create<Street>().ParseAll(osmData, string.Empty)),
			Task.Run(() => houses = OsmParserFactory.Create<House>().ParseAll(osmData, string.Empty)));
		await ObjectLinkBuilder.LinkAddressElements(area, districts!, cities!, villages!, streets!, houses!);

		var resultHouses = UnusedAddressesCleaner.Clean(houses);

		return resultHouses;
	}
}
