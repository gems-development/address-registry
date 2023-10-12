using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class CityDataSource : DataSourceBase
    {
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
    }
}
