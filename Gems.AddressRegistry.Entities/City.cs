using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class City : BaseGeoEntity
    {
        public virtual ICollection<CityDataSource> DataSources { get; set; } = new List<CityDataSource>(0);
        public String Name { get; set; } = null!;
    }
}
