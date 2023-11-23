using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class TerritoryDataSource : DataSourceBase
    {
        public Guid TerritoryId { get; set; }
        public virtual Territory Territory { get; set; } = null!;
    }
}
