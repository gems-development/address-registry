using Gems.AddressRegistry.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gems.AddressRegistry.Entities.Common
{
    abstract public class DataSourceBase
    {
        public String Id { get; set; } = null!;
        [NotMapped]
        public string? AuxiliaryId { get; set; }
        public SourceType SourceType { get; set; }
    }
}
