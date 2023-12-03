using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Settlement : BaseGeoEntity
    {
        public virtual ICollection<SettlementDataSource> DataSources { get; set; } = new List<SettlementDataSource>(0);
        public String Name { get; set; } = null!;
    }
}
