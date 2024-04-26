using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.AddressRegistry.Entities.Enums;

namespace Gems.AddressRegistry.Entities
{
    public class Building : BaseGeoEntity
    {
        public virtual ICollection<BuildingDataSource> DataSources { get; set; } = new List<BuildingDataSource>(0);
        public int Postcode { get; set; }
        public string Number { get; set; } = null!;
        public RoadNetworkElement? RoadNetworkElement { get; set; }
        public PlaningStructureElement? PlaningStructureElement { get; set; }
        public BuildingType BuildingType { get; set; }
    }
}
