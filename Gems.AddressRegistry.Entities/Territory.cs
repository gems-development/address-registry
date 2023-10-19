using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Territory : BaseAuditableEntity
    {
        public virtual ICollection<TerritoryDataSource> DataSources { get; set; }
        public String Name { get; set; }
    }
}
