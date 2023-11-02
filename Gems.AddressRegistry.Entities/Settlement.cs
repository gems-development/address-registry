using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Settlement : BaseGeoEntity
    {
        public virtual ICollection<SettlementDataSource> DataSources { get; set; }
        public String Name { get; set; }
    }
}
