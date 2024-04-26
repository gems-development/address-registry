using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class PlaningStructureElement : BaseGeoEntity
    {
        public virtual ICollection<EpsDataSource> DataSources { get; set; } = new List<EpsDataSource>(0);
        public virtual City? City { get; set; }
        public virtual Settlement? Settlement { get; set; }
        public string Name { get; set; } = null!;
    }
}
