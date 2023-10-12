using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class AdministrativeAreaDataSource:DataSourceBase
    {
        public Guid AdministrativeAreaId { get; set; }
        public virtual AdministrativeArea AdministrativeArea { get; set; }
    }
}
