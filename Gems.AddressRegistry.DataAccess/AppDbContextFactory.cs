namespace Gems.AddressRegistry.DataAccess
{
    internal class AppDbContextFactory : IAppDbContextFactory
    {
        private readonly string _connectionString;


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
