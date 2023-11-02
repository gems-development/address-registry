using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class PlaningStructureElement : BaseGeoEntity
    {
        public virtual ICollection<EpsDataSource> DataSources { get; set; }
        public String Name { get; set; }
    }
}
