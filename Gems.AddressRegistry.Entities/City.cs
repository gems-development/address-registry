using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class City : BaseAuditableEntity
    {
        public virtual ICollection<CityDataSource> DataSources { get; set; }
        public String Name { get; set; }
    }
}
