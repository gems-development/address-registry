using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities
{
    public class City : BaseAuditableEntity
    {
        public string Name { get; set; }
        public enum Type { City, village, etc}
    }
}
