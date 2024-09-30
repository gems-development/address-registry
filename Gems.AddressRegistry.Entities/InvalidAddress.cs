namespace Gems.AddressRegistry.Entities
{
    public class InvalidAddress : Address
    {
        public InvalidAddress() { }

        public InvalidAddress(Address address) 
        {
            this.DataSources = address.DataSources;
            this.Region = address.Region;
            this.MunicipalArea = address.MunicipalArea;
            this.AdministrativeArea = address.AdministrativeArea;
            this.Territory = address.Territory;
            this.City = address.City;
            this.Settlement = address.Settlement;
            this.PlaningStructureElement = address.PlaningStructureElement;
            this.RoadNetworkElement = address.RoadNetworkElement;
            this.Building = address.Building;
        }
    }
}
