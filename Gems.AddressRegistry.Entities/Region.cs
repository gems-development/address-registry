using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities
{
    public class Region : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
