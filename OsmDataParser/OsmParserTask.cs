using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.OsmDataParser.DataGroupingServices;
using Gems.AddressRegistry.OsmDataParser.Factories;
using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataParser;

public static class OsmParserTask
{
	public static async Task<IReadOnlyCollection<House>> Execute(string pathToPbf, string areaName, ILogger logger)
	{
		var osmData = await OsmDataReader.Read(pathToPbf, logger);
		var area = OsmParserFactory.Create<Area>().Parse(osmData, areaName, logger, default!);
		IReadOnlyCollection<District>? districts = null;
		IReadOnlyCollection<City>? cities = null;
		IReadOnlyCollection<Village>? villages = null;
		IReadOnlyCollection<Street>? streets = null;
		IReadOnlyCollection<House>? houses = null;

		await Task.WhenAll(
			Task.Run(() => districts = OsmParserFactory.Create<District>().ParseAll(osmData, logger, areaName)),
			Task.Run(() => cities = OsmParserFactory.Create<City>().ParseAll(osmData, logger, string.Empty)),
			Task.Run(() => villages = OsmParserFactory.Create<Village>().ParseAll(osmData, logger, string.Empty)),
			Task.Run(() => streets = OsmParserFactory.Create<Street>().ParseAll(osmData, logger, string.Empty)),
			Task.Run(() => houses = OsmParserFactory.Create<House>().ParseAll(osmData, logger, string.Empty)));
		await ObjectLinkBuilder.LinkAddressElements(area, districts!, cities!, villages!, streets!, houses!, logger);

		var resultHouses = UnusedAddressesCleaner.Clean(houses!, logger);

		return resultHouses;
	}
}
