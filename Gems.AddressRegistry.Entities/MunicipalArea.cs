using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class MunicipalArea : BaseGeoEntity
    {
        public virtual ICollection<MunicipalAreaDataSource> DataSources { get; set; } = new List<MunicipalAreaDataSource>(0);
        public virtual Region Region { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
