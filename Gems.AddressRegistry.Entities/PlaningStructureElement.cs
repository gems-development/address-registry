using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class PlaningStructureElement : BaseAuditableEntity
    {
        public virtual ICollection<EpsDataSource> DataSources { get; set; }
        public string Name { get; set; }
    }
}
