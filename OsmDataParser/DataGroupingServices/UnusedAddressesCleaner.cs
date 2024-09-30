using Gems.AddressRegistry.OsmDataParser.Model;
using Microsoft.Extensions.Logging;

namespace Gems.AddressRegistry.OsmDataParser.DataGroupingServices;

public static class UnusedAddressesCleaner
{
	public static IReadOnlyCollection<House> Clean(IReadOnlyCollection<House> houses, ILogger logger)
	{
		logger.LogDebug("OSM || Начата чистка адресов");

		var resultHouses = new List<House>();

		foreach (var house in houses)
		{
			var cityAddressAssembly = house.Street?.City?.District?.Area;
			var villageA​ddressAssembly = house.Street?.Village?.District?.Area;

			if (cityA​ddressAssembly is not null || villageA​ddressAssembly is not null)
				resultHouses.Add(house);
		}
        logger.LogDebug("OSM || Чистка адресов завершена");

        return resultHouses;
	}
}
