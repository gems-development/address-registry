using System.Diagnostics;
using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataParser.DataGroupingServices;

public static class ObjectLinkBuilder
{
    public static void LinkAddressElements(Area area, 
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
            
            foreach (var city in cities)
            {
                if (ObjectIntersector.Intersects(district, city))
                {
                    city.District = district;
                    Debug.WriteLine($"Городу <{city.Name}> присвоен район <{district.Name}>");
                }

                foreach (var street in streets)
                {
                    if (ObjectIntersector.Intersects(city, street) && street.City == null)
                    {
                        street.City = city;
                        Debug.WriteLine($"Улице <{street.Name}> присвоен город <{city.Name}>");
                    }

                    foreach (var house in houses
                                 .Where(house => house.Name == street.Name 
                                                 && ObjectIntersector.Intersects(city, house)
                                                 && house.Street == null))
                    {
                        house.Street = street;
                        Debug.WriteLine($"Дому <{house.Number}> присвоена улица <{street.Name}>");
                    }
                }
            }

            foreach (var village in villages)
            {
                if (ObjectIntersector.Intersects(district, village))
                {
                    village.District = district;
                    Debug.WriteLine($"Селу <{village.Name}> присвоен район <{district.Name}>");
                }
            
                foreach (var street in streets)
                {
                    if (ObjectIntersector.Intersects(village, street) && street.Village == null)
                    {
                        street.Village = village;
                        Debug.WriteLine($"Улице <{street.Name}> присвоено село <{village.Name}>");
                    }

                    foreach (var house in houses
                                 .Where(house => (house.Name == street.Name) 
                                                 && ObjectIntersector.Intersects(village, house)
                                                 && house.Street == null))
                    {
                        house.Street = street;
                        Debug.WriteLine($"Дому <{house.Number}> присвоена улица <{street.Name}>");
                    }
                }
            }
        }
    }
}