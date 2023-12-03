using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class LandPlot : BaseGeoEntity
    {
        public virtual ICollection<LandPlotDataSource> DataSources { get; set; } = new List<LandPlotDataSource>(0);
        public String Name { get; set; } = null!;
    }
}
