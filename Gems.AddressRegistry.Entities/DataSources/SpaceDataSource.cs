using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class SpaceDataSource : DataSourceBase
    {
        public Guid SpaceId { get; set; }
        public virtual Space Space { get; set; }
    }
}
