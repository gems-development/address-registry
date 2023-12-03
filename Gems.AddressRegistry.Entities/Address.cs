using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Address : BaseGeoEntity
    {
        public virtual ICollection<AddressDataSource> DataSources { get; set; } = new List<AddressDataSource>(0);
        public virtual Country Country { get; set; } = null!;
        public virtual Region Region { get; set; } = null!;
        public virtual MunicipalArea MunicipalArea { get; set; } = null!;
        public virtual AdministrativeArea? AdministrativeArea { get; set; }
        public virtual Territory Territory { get; set; } = null!;
        public virtual City? City { get; set; }
        public virtual Settlement Settlement { get; set; } = null!;
        public virtual PlaningStructureElement? PlaningStructureElement { get; set; }
        public virtual RoadNetworkElement? RoadNetworkElement { get; set; }
        public virtual LandPlot? LandPlot { get; set; }
        public virtual Building? Building { get; set; }
        public virtual Space? Space { get; set; }

    }
}