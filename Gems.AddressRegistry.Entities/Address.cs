using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities
{
    public class Address : BaseAuditableEntity
    {
        public string ExternalId { get; set; }

        public Country Country { get; set; }
        public Region Region { get; set; }
        public AdministrativeArea AdministrativeArea { get; set; }
        public MunicipalArea MunicipalArea { get; set; }
        public Territory Territory { get; set; }
        public City City { get; set; }
        public Settlement Settlement { get; set; }
        public EPS EPS { get; set; }
        public ERN ERN { get; set; }
        public LandPlot LandPlot { get; set; }
        public Building Building { get; set; }
        public Space Space { get; set; }

    }
}