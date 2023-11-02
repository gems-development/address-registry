using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Region : BaseGeoEntity
    {
        public virtual ICollection<RegionDataSource> DataSources { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
    }
}
