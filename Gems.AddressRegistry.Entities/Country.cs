using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Country : BaseGeoEntity
    {
        public virtual ICollection<CountryDataSource> DataSources { get; set; } = new List<CountryDataSource>(0);
        public String Name { get; set; } = null!;
    }
}
