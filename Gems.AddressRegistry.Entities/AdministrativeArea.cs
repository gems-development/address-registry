using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class AdministrativeArea : BaseGeoEntity
    {
        public virtual ICollection<AdministrativeAreaDataSource> DataSources { get; set; }
        public String Name { get; set; }
    }
}
