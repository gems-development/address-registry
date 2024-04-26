using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class Settlement : BaseGeoEntity
    {
        public virtual ICollection<SettlementDataSource> DataSources { get; set; } = new List<SettlementDataSource>(0);
        public virtual City? City { get; set; }
        public virtual Territory? Territory { get; set; }
        public virtual MunicipalArea? MunicipalArea { get; set; }
        public string Name { get; set; } = null!;
    }
}
