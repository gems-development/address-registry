using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.AddressRegistry.Entities.Enums;

namespace Gems.AddressRegistry.Entities
{
    public class Building : BaseGeoEntity
    {

        public virtual ICollection<BuildingDataSource> DataSources { get; set; }
        public int Postcode { get; set; }
        public int Number { get; set; }
        public BuildingType BuildingType { get; set; }


    }
}
