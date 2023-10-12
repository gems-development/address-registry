using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class CountryDataSource : DataSourceBase
    {
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
