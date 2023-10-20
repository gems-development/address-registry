using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Country : BaseAuditableEntity
    {
        public virtual ICollection<CountryDataSource> DataSources { get; set; }
        public String Name { get; set; }
    }
}
