using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class SettlementDataSource : DataSourceBase
    {
        public Guid SettlementId { get; set; }
        public virtual Settlement Settlement { get; set; } = null!;
    }
}
