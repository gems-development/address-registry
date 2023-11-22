namespace Gems.AddressRegistry.DataAccess
{
    public class AppDbContextFactory : IAppDbContextFactory
    {
        private readonly string _connectionString;

        public AppDbContextFactory()
        {
        }

        public AppDbContextFactory(string connectionString) 
        {
            _connectionString = connectionString;
        } 
        public IAppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
