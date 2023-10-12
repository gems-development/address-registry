using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class ErnDataSource : DataSourceBase
    {
        public Guid ErnId { get; set; }
        public virtual RoadNetworkElement Ern { get; set; }
    }
}
