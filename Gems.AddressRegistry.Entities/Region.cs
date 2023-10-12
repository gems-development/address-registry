using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Region : BaseAuditableEntity
    {
        public virtual ICollection<RegionDataSource> DataSources { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
