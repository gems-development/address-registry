using Gems.AddressRegistry.Entities;

namespace ApplicationServices.UseCases.Requests.GetAddressById
{
    public class AddressResponseDto
    {
        public String NameOfCountry { get; set; }
        public String NameOfRegion { get; set; }
        public String? NameOfAdministrativeArea { get; set; }
        public String NameOfMunicipalArea { get; set; }
        public String NameOfTerritory { get; set; }
        public String? NameOfCity { get; set; }
        public String NameOfSettlement { get; set; }
        public String? NameOfPlaningStructureElement { get; set; }
        public String? NameOfRoadNetworkElement { get; set; }
        public String? TypeOfRoadNetworkElement { get; set; }
        public String? NameOfLandPlot { get; set; }
        public String? NumberOfBuilding { get; set; }
        public String? TypeOfBuilding { get; set; }
        public String? NumberOfSpace { get; set; }
        public String? TypeOfSpace { get; set; }

        public AddressResponseDto(Address address)
        {
            NameOfCountry = address.Country.Name;
            NameOfRegion = address.Region.Name;
            NameOfAdministrativeArea = address.AdministrativeArea.Name;
            NameOfMunicipalArea = address.MunicipalArea.Name;
            NameOfTerritory = address.Territory.Name;
            NameOfCity = address.City.Name;
            NameOfSettlement = address.Settlement.Name;
            NameOfPlaningStructureElement = address.PlaningStructureElement.Name;
            NameOfRoadNetworkElement = address.RoadNetworkElement.Name;
            TypeOfRoadNetworkElement = address.RoadNetworkElement.RoadNetworkElementType.ToString();
            NameOfLandPlot = address.LandPlot.Name;
            NumberOfBuilding = address.Building.Number;
            TypeOfBuilding = address.Building.BuildingType.ToString();
            NumberOfSpace = address.Space.Number;
            TypeOfSpace = address.Space.SpaceType.ToString();
        }
    }

    
}
