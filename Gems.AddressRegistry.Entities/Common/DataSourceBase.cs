using Gems.AddressRegistry.Entities.Enums;

namespace Gems.AddressRegistry.Entities.Common
{
    abstract public class DataSourceBase
    {
        public String Id { get; set; }
        public SourceType SourceType { get; set; }
    }
}
