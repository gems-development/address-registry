using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class PlaningStructureElement : BaseGeoEntity
    {
        public virtual ICollection<EpsDataSource> DataSources { get; set; } = new List<EpsDataSource>(0);
        public City? City { get; set; }
        public Settlement? Settlement { get; set; }
        public String Name { get; set; } = null!;
    }
}
