using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataGroupingService;

public static class ObjectGrouper
{
    public static void GroupAll(Area area, 
        List<District> districts, 
        List<Settlement> settlements,
        List<City> cities,
        List<Village> villages,
        List<Street> streets,
        List<House> houses)
    {
        foreach (var settlement in settlements)
        {
            foreach (var city in cities)
            {
                if (ObjectIntersector.Intersects(settlement, city))
                    city.Settlement = settlement;

                foreach (var street in streets)
                {
                    if (ObjectIntersector.Intersects(city, street))
                        street.City = city;

                    foreach (var house in houses.Where(house => house.Name == street.Name))
                        house.Street = street;
                }
            }

            foreach (var village in villages)
            {
                if (ObjectIntersector.Intersects(settlement, village))
                    village.Settlement = settlement;
                
                foreach (var street in streets)
                {
                    if (ObjectIntersector.Intersects(village, street))
                        street.Village = village;

                    foreach (var house in houses.Where(house => house.Name == street.Name))
                        house.Street = street;
                }
            }
        }
    }
}