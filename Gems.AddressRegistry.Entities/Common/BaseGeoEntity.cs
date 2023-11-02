namespace Gems.AddressRegistry.Entities.Common
{
    abstract public class BaseGeoEntity : BaseAuditableEntity
    {
        public String GeoJson { get; set; }
    }
}
