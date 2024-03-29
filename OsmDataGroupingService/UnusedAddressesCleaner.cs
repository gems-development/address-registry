using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataGroupingService;

public static class UnusedAddressesCleaner
{
    public static IReadOnlyCollection<House> Clean(IReadOnlyCollection<House> houses)
    {
        var resultHouses = new List<House>();
        
        foreach (var house in houses)
        {
            if (house.Street is not null)
                resultHouses.Add(house);
        }

        return resultHouses;
    }
}