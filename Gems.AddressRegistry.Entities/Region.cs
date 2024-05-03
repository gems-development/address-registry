using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Region : BaseGeoEntity
    {
        public virtual ICollection<RegionDataSource> DataSources { get; set; } = new List<RegionDataSource>();
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
