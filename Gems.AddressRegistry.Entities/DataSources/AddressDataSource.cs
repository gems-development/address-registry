using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class AddressDataSource : DataSourceBase
    {
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
