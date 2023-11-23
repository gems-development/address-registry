using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Territory : BaseGeoEntity
    {
        public virtual ICollection<TerritoryDataSource> DataSources { get; set; } = new List<TerritoryDataSource>(0);
        public String Name { get; set; } = null!;
    }
}
