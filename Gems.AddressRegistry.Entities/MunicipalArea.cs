using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class MunicipalArea : BaseAuditableEntity
    {
        public virtual ICollection<MunicipalAreaDataSource> DataSources { get; set; }
        public String Name { get; set; }
    }
}
