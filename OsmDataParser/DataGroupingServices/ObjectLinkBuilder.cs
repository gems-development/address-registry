using System.Diagnostics;
using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataParser.DataGroupingServices;

public static class ObjectLinkBuilder
{
	public static async Task LinkAddressElements(
		Area area,
		IReadOnlyCollection<District> districts,
		IReadOnlyCollection<City> cities,
		IReadOnlyCollection<Village> villages,
		IReadOnlyCollection<Street> streets,
		IReadOnlyCollection<House> houses)
	{
		foreach (var district in districts)
		{
			district.Area = area;

			Debug.WriteLine($"Району <{district.Name}> присвоена область <{area.Name}>");

			await Task.WhenAll(
				Task.Run(() => LinkByCities(district, cities, streets, houses)),
				Task.Run(() => LinkByVillages(district, villages, streets, houses)));
		}
	}

	private static void LinkByCities(
		District district,
		IReadOnlyCollection<City> cities,
		IReadOnlyCollection<Street> streets,
		IReadOnlyCollection<House> houses)
	{
		foreach (var city in cities)
		{
			if (ObjectIntersector.Intersects(district, city))
			{
				city.District = district;

				Debug.WriteLine($"Городу <{city.Name}> присвоен район <{district.Name}>");
			}

			foreach (var street in streets)
			{
				if (street.City is null && ObjectIntersector.Intersects(city, street))
				{
					street.City = city;

					Debug.WriteLine($"Улице <{street.Name}> присвоен город <{city.Name}>");
				}

				foreach (var house in houses
					.Where(house => house.Street is null
						&& house.Name == street.Name
						&& ObjectIntersector.Intersects(city, house)))
				{
					house.Street = street;

					Debug.WriteLine($"Дому <{house.Number}> присвоена улица <{street.Name}>");
				}
			}
		}
	}

	private static void LinkByVillages(
		District district,
		IReadOnlyCollection<Village> villages,
		IReadOnlyCollection<Street> streets,
		IReadOnlyCollection<House> houses)
	{
		foreach (var village in villages)
		{
			if (ObjectIntersector.Intersects(district, village))
			{
				village.District = district;

				Debug.WriteLine($"Селу <{village.Name}> присвоен район <{district.Name}>");
			}

			foreach (var street in streets)
			{
				if (street.Village is null && ObjectIntersector.Intersects(village, street))
				{
					street.Village = village;

					Debug.WriteLine($"Улице <{street.Name}> присвоено село <{village.Name}>");
				}

				foreach (var house in houses
					.Where(house => house.Street is null
						&& house.Name == street.Name 
						&& ObjectIntersector.Intersects(village, house)))
				{
					house.Street = street;

					Debug.WriteLine($"Дому <{house.Number}> присвоена улица <{street.Name}>");
				}
			}
		}
	}
}
