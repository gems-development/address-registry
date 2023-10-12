using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class AdministrativeArea : BaseAuditableEntity
    {
        public virtual ICollection<AdministrativeAreaDataSource> DataSources { get; set; }
        public string Name { get; set; }
    }
}
