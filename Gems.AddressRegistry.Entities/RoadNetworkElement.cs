using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.AddressRegistry.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gems.AddressRegistry.Entities
{
    public class RoadNetworkElement : BaseGeoEntity
    {
        public virtual ICollection<ErnDataSource> DataSources { get; set; } = new List<ErnDataSource>(0);
        public RoadNetworkElementType RoadNetworkElementType { get; set; }
        public string Name { get; set; } = null!;
        public virtual City? City { get; set; }
        public virtual Settlement? Settlement { get; set; }
        public virtual PlaningStructureElement? PlaningStructureElement { get; set; }
        [NotMapped]
        public string NormalizedName { get; set; } = null!;
    }
}
