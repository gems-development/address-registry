using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.AddressRegistry.Entities
{
    public class LandPlot : BaseAuditableEntity
    {
        public virtual ICollection<LandPlotDataSource> DataSources { get; set; }
        public string Name { get; set; }
    }
}
