using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.Entities.DataSources
{
    public class LandPlotDataSource : DataSourceBase
    {
        public Guid LandPlotId { get; set; }
        public virtual LandPlot LandPlot { get; set; }
    }
}
