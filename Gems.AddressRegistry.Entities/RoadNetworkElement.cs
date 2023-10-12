using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.AddressRegistry.Entities.Enums;

namespace Gems.AddressRegistry.Entities
{
    public class RoadNetworkElement : BaseAuditableEntity
    {
        public virtual ICollection<ErnDataSource> DataSources { get; set; }
        public RoadNetworkElementType RoadNetworkElementType { get; set; }
        public string Name { get; set; }
    }
}
