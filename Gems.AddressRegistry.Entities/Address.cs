using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities
{
    public class Address : BaseAuditableEntity
    {
        public string ExternalId { get; set; }

        public Country Country { get; set; }
        public Region Region { get; set; }
        public City City { get; set; }  
        public District District { get; set; }
        public Street? Street { get; set; }
        public House House { get; set; }

    }
}