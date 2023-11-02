using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class MunicipalArea : BaseGeoEntity
    {
        public virtual ICollection<MunicipalAreaDataSource> DataSources { get; set; }
        public String Name { get; set; }
    }
}
