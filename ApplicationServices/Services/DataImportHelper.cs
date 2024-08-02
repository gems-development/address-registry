using Gems.AddressRegistry.Entities;

namespace Gems.ApplicationServices.Services
{
    internal static class DataImportHelper
    {
        public static void Map (Building source,  Building target)
        {
            target.Number = source.Number;
        }
        public static void Map(RoadNetworkElement source, RoadNetworkElement target)
        {
            target.Name = source.Name;
            target.RoadNetworkElementType = source.RoadNetworkElementType;
        }
        public static void Map(PlaningStructureElement source, PlaningStructureElement target)
        {
            target.Name = source.Name;
        }
        public static void Map(Settlement source, Settlement target)
        {
            target.Name = source.Name;
        }
        public static void Map(City source, City target)
        {
            target.Name = source.Name;
        }
        public static void Map(Territory source, Territory target)
        {
            target.Name = source.Name;
        }
        public static void Map(MunicipalArea source, MunicipalArea target)
        {
            target.Name = source.Name;
        }
        public static void Map(AdministrativeArea source, AdministrativeArea target)
        {
            target.Name = source.Name;
        }
        public static void Map(Region source, Region target)
        {
            target.Name = source.Name;
        }
        public static void Map(Country source, Country target)
        {
            target.Name = source.Name;
        }
    }
}
