using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities
{
    public class Building : BaseAuditableEntity
    {
        public int Postcode { get; set; }
        public int Number { get; set; }

        public enum Type { House, Apartament, etc }
    }
}
