namespace Gems.AddressRegistry.DataAccess
{
    public class AppDbContextFactory : IAppDbContextFactory
    {
        public IAppDbContext Create() 
        { 
            return new AppDbContext();
        }
    }
}
