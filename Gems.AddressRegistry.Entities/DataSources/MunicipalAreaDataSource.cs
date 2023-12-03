using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class MunicipalAreaDataSource : DataSourceBase
    {
        public Guid MunicipalAreaId { get; set; }
        public virtual MunicipalArea MunicipalArea { get; set; } = null!;
    }
}
