using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Region : BaseGeoEntity
    {
        public virtual ICollection<RegionDataSource> DataSources { get; set; } = new List<RegionDataSource>();
        public String Name { get; set; } = null!;
        public String Code { get; set; } = null!;
    }
}
