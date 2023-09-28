using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities
{
    public class House : BaseAuditableEntity
    {
        public int Postcode { get; set; }
        public int Number { get; set; }
    }
}
