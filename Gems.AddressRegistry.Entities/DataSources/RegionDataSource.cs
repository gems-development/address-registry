using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class RegionDataSource : DataSourceBase
    {
        public Guid RegionId { get; set; }
        public virtual Region Region { get; set; } = null!;
    }
}
