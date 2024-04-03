using System.Text;
using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataGroupingService;

public static class NormalizedAddressBuilder
{
    private const string AddressPartDivider = "#";
    public static void BuildAddress(IReadOnlyCollection<House> houses)
    {
        foreach (var house in houses)
        {
            var normalizedAddress = new StringBuilder();

            var houseNumber = house.Number;
            var street = house.Street;
            
            normalizedAddress.Insert(0, houseNumber);
            normalizedAddress.Insert(0, AddressPartDivider);
            normalizedAddress.Insert(0, street!.Name);

            if (street.City is not null)
            {
                var city = street.City;
                normalizedAddress.Insert(0, AddressPartDivider);
                normalizedAddress.Insert(0, city.Name);
                
                var district = city.District;
                normalizedAddress.Insert(0, AddressPartDivider);
                normalizedAddress.Insert(0, district.Name);
                
                var area = district.Area;
                normalizedAddress.Insert(0, AddressPartDivider);
                normalizedAddress.Insert(0, area.Name);
            }
            else if (street.Village is not null)
            {
                var village = street.Village;
                normalizedAddress.Insert(0, AddressPartDivider);
                normalizedAddress.Insert(0, village.Name);
                
                var district = village.District;
                normalizedAddress.Insert(0, AddressPartDivider);
                normalizedAddress.Insert(0, district.Name);
                
                var area = district.Area;
                normalizedAddress.Insert(0, AddressPartDivider);
                normalizedAddress.Insert(0, area.Name);
            }

            var resultAddress = normalizedAddress.ToString();
            house.Address = resultAddress;
        }
    }
}