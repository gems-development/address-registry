using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.AddressRegistry.Entities.Enums;

namespace Gems.AddressRegistry.Entities
{
    public class Space : BaseGeoEntity
    {
        public virtual ICollection<SpaceDataSource> DataSources { get; set; } = new List<SpaceDataSource>(0);
        public String Number { get; set; } = null!;

        public SpaceType SpaceType { get; set; }
    }
}
