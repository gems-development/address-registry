namespace Gems.AddressRegistry.DataAccess
{
    public interface IAppDbContextFactory
    {
        IAppDbContext Create();
    }
}
