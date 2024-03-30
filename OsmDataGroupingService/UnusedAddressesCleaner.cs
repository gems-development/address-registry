using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataGroupingService;

public static class UnusedAddressesCleaner
{
    public static IReadOnlyCollection<House> Clean(IReadOnlyCollection<House> houses)
    {
        var resultHouses = new List<House>();
        
        foreach (var house in houses)
        {
            var citySeq = house.Street?.City?.District?.Area;
            var villageSeq = house.Street?.Village?.District?.Area;
            
            if (citySeq != null)
                resultHouses.Add(house);
            
            else if (villageSeq != null)
                resultHouses.Add(house);
        }

        return resultHouses;
    }
}