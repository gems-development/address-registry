using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class BuildingDataSource : DataSourceBase
    {
        public Guid BuildingId { get; set; }
        public virtual Building Building { get; set; } = null!;
    }
}
