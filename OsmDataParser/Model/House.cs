using System.Text;
using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.OsmDataParser.Interfaces;

namespace Gems.AddressRegistry.OsmDataParser.Model;

public class House : RealObject, INormalizable
{
	public string Number { get; init; } = null!;
	public Street? Street { get; set; }
	public string Address { get; set; } = null!;

	public string GetNormalizedAddress(ILogger logger)
	{
		const string addressPartDivider = "#";
		var normalizedAddress = new StringBuilder();
		var houseNumber = Number;
		var street = Street;

		normalizedAddress.Insert(0, houseNumber);
		normalizedAddress.Insert(0, addressPartDivider);
		normalizedAddress.Insert(0, street!.Name);

		if (street.City is not null)
		{
			var city = street.City;
			normalizedAddress.Insert(0, addressPartDivider);
			normalizedAddress.Insert(0, city.Name);

			var district = city.District;
			normalizedAddress.Insert(0, addressPartDivider);
			normalizedAddress.Insert(0, district.Name);

			var area = district.Area;
			normalizedAddress.Insert(0, addressPartDivider);
			normalizedAddress.Insert(0, area.Name);
		}
		else if (street.Village is not null)
		{
			var village = street.Village;
			normalizedAddress.Insert(0, addressPartDivider);
			normalizedAddress.Insert(0, village.Name);

			var district = village.District;
			normalizedAddress.Insert(0, addressPartDivider);
			normalizedAddress.Insert(0, district.Name);

			var area = district.Area;
			normalizedAddress.Insert(0, addressPartDivider);
			normalizedAddress.Insert(0, area.Name);
		}

		var resultAddress = normalizedAddress.ToString().ToUpper();
		Address = resultAddress;
		logger.LogTrace($"Построен адрес: {resultAddress}");

		return Address;
	}
}
