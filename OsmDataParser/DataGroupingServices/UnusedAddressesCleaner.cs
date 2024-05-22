using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataParser.DataGroupingServices;

public static class UnusedAddressesCleaner
{
	public static IReadOnlyCollection<House> Clean(IReadOnlyCollection<House> houses)
	{
		var resultHouses = new List<House>();

		foreach (var house in houses)
		{
			var citySeq = house.Street?.City?.District.Area;
			var villageSeq = house.Street?.Village?.District.Area;

			if (citySeq is not null || villageSeq is not null)
				resultHouses.Add(house);
		}

		return resultHouses;
	}
}
