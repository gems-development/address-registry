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
        public String Name { get; set; } = null!;
        [NotMapped]
        public String NormalizedName { get; set; } = null!;
    }
}
