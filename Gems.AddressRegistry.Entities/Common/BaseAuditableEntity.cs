namespace Gems.AddressRegistry.Entities.Common
{
    public abstract class BaseAuditableEntity : BaseIdentifiableEntity
    {
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
