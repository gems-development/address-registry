using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataGroupingService;

public static class ObjectLinkBuilder
{
    public static void LinkAddressElements(Area area, 
        IReadOnlyCollection<District> districts, 
        IReadOnlyCollection<Settlement> settlements,
        IReadOnlyCollection<City> cities,
        IReadOnlyCollection<Village> villages,
        IReadOnlyCollection<Street> streets,
        IReadOnlyCollection<House> houses)
    {
        foreach (var district in districts)
        {
            district.Area = area;
            Console.WriteLine($"Району <{district.Name}> присвоена область <{area.Name}>");
            
            foreach (var settlement in settlements)
            {
                if (settlement.DistrictId == district.Id)
                {
                    settlement.District = district;
                    Console.WriteLine($"Поселению <{settlement.Name}> присвоен район <{district.Name}>");
                }
                
                foreach (var city in cities)
                {
                    if (ObjectIntersector.Intersects(settlement, city))
                    {
                        city.Settlement = settlement;
                        Console.WriteLine($"Городу <{city.Name}> присвоено поселение <{settlement.Name}>");
                    }

                    foreach (var street in streets)
                    {
                        if (ObjectIntersector.Intersects(city, street))
                        {
                            street.City = city;
                            Console.WriteLine($"Улице <{street.Name}> присвоен город <{city.Name}>");
                        }

                        foreach (var house in houses.Where(house => house.Name == street.Name))
                        {
                            house.Street = street;
                            Console.WriteLine($"Дому <{house.Number}> присвоена улица <{street.Name}>");
                        }
                    }
                }

                foreach (var village in villages)
                {
                    if (ObjectIntersector.Intersects(settlement, village))
                    {
                        village.Settlement = settlement;
                        Console.WriteLine($"Селу <{village.Name}> присвоено поселение <{settlement.Name}>");
                    }
                
                    foreach (var street in streets)
                    {
                        if (ObjectIntersector.Intersects(village, street))
                        {
                            street.Village = village;
                            Console.WriteLine($"Улице <{street.Name}> присвоено село <{village.Name}>");
                        }

                        foreach (var house in houses.Where(house => house.Name == street.Name))
                        {
                            house.Street = street;
                            Console.WriteLine($"Дому <{house.Number}> присвоена улица <{street.Name}>");
                        }
                    }
                }
            }
        }
    }
}