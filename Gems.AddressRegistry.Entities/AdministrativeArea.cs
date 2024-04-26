using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class AdministrativeArea : BaseGeoEntity
    {
        public virtual ICollection<AdministrativeAreaDataSource> DataSources { get; set; } = new List<AdministrativeAreaDataSource>(0);
        public virtual Region Region { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
