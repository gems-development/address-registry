using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataParser.DataGroupingServices;

public static class UnusedAddressesCleaner
{
	public static IReadOnlyCollection<House> Clean(IReadOnlyCollection<House> houses)
	{
		var resultHouses = new List<House>();

		foreach (var house in houses)
		{
			var cityAddressAssembly = house.Street?.City?.District?.Area;
			var villageA​ddressAssembly = house.Street?.Village?.District?.Area;

			if (cityA​ddressAssembly is not null || villageA​ddressAssembly is not null)
				resultHouses.Add(house);
		}

		return resultHouses;
	}
}
