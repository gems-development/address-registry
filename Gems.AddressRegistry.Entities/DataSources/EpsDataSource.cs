using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class EpsDataSource : DataSourceBase
    {
        public Guid EpsId { get; set; }
        public virtual PlaningStructureElement Eps { get; set; }
    }
}
