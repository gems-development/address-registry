using Gems.AddressRegistry.Entities;

namespace WebApi.Dto
{
    public class AddressDtoResponse
    {
        public string NameOfRegion { get; set; }
        public string? NameOfAdministrativeArea { get; set; }
        public string? NameOfMunicipalArea { get; set; }
        public string? NameOfTerritory { get; set; }
        public string? NameOfCity { get; set; }
        public string? NameOfSettlement { get; set; }
        public string? NameOfPlaningStructureElement { get; set; }
        public string? NameOfRoadNetworkElement { get; set; }
        public string? TypeOfRoadNetworkElement { get; set; }
        
        public string? NumberOfBuilding { get; set; }
        public string? TypeOfBuilding { get; set; }
        

        public AddressDtoResponse(Address address)
        {
            NameOfRegion = address.Region.Name;
            NameOfAdministrativeArea = address.AdministrativeArea?.Name;
            NameOfMunicipalArea = address.MunicipalArea?.Name;
            NameOfTerritory = address.Territory?.Name;
            NameOfCity = address.City?.Name;
            NameOfSettlement = address.Settlement?.Name;
            NameOfPlaningStructureElement = address.PlaningStructureElement?.Name;
            NameOfRoadNetworkElement = address.RoadNetworkElement?.Name;
            TypeOfRoadNetworkElement = address.RoadNetworkElement?.RoadNetworkElementType.ToString();
            
            NumberOfBuilding = address.Building?.Number;
            TypeOfBuilding = address.Building?.BuildingType.ToString();
        }
    }
}
