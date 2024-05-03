using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class City : BaseGeoEntity
    {
        public virtual ICollection<CityDataSource> DataSources { get; set; } = new List<CityDataSource>(0);
        public virtual Territory? Territory { get; set; }
        public virtual MunicipalArea? MunicipalArea { get; set; }
        public virtual AdministrativeArea? AdministrativeArea { get; set; }
        public string Name { get; set; } = null!;
    }
}
