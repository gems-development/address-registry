using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class City : BaseAuditableEntity
    {
        public virtual ICollection<CityDataSource> DataSources { get; set; }
        public string Name { get; set; }
    }
}
