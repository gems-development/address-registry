using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class AdministrativeArea : BaseAuditableEntity
    {
        public virtual ICollection<AdministrativeAreaDataSource> DataSources { get; set; }
        public String Name { get; set; }
    }
}
