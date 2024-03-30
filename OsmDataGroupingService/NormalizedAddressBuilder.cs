using System.Text;
using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataGroupingService;

public static class NormalizedAddressBuilder
{
    private const char AddressPartDivider = '#';
    public static void BuildAddress(IReadOnlyCollection<House> houses)
    {
        foreach (var house in houses)
        {
            var normalizedAddress = new StringBuilder(house.Number);
            
            var street = house.Street;
            normalizedAddress.Append(AddressPartDivider);
            normalizedAddress.Append(street!.Name);

            if (street.City is not null)
            {
                var city = street.City;
                normalizedAddress.Append(AddressPartDivider);
                normalizedAddress.Append(city.Name);
                
                var district = city.District;
                normalizedAddress.Append(AddressPartDivider);
                normalizedAddress.Append(district.Name);
                
                var area = district.Area;
                normalizedAddress.Append(AddressPartDivider);
                normalizedAddress.Append(area.Name);
            }
            else if (street.Village is not null)
            {
                var village = street.Village;
                normalizedAddress.Append(AddressPartDivider);
                normalizedAddress.Append(village.Name);
                
                var district = village.District;
                normalizedAddress.Append(AddressPartDivider);
                normalizedAddress.Append(district.Name);
                
                var area = district.Area;
                normalizedAddress.Append(AddressPartDivider);
                normalizedAddress.Append(area.Name);
            }

            var resultAddress = normalizedAddress.ToString();
            house.Address = resultAddress;
        }
    }
}