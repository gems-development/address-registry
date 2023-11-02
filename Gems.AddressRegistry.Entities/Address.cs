using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Address : BaseGeoEntity
    {
        public virtual ICollection<AddressDataSource> DataSources { get; set; }
        public Country Country { get; set; }
        public Region Region { get; set; }
        public AdministrativeArea? AdministrativeArea { get; set; }
        public MunicipalArea MunicipalArea { get; set; }
        public Territory Territory { get; set; }
        public City? City { get; set; }
        public Settlement Settlement { get; set; }
        public PlaningStructureElement? PlaningStructureElement { get; set; }
        public RoadNetworkElement? RoadNetworkElement { get; set; }
        public LandPlot? LandPlot { get; set; }
        public Building? Building { get; set; }
        public Space? Space { get; set; }

    }
}