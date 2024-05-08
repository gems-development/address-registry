namespace Gems.AddressRegistry.DataAccess
{
    public class AppDbContextFactory : IAppDbContextFactory
    {
        private readonly string _connectionString;
        private readonly bool _asNoTracking;

        public AppDbContextFactory(bool asNoTracking)
        {
            _asNoTracking = asNoTracking;
        }
        
        public AppDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IAppDbContext Create()
        {
            return new AppDbContext(_connectionString);
        }
    }
}
