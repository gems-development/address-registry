using Microsoft.EntityFrameworkCore;

namespace Gems.AddressRegistry.DataAccess
{
	public class AppDbContextFactory : IAppDbContextFactory
	{
		private readonly string _connectionString;
		private readonly bool _asNoTracking;

		public AppDbContextFactory(string connectionString, bool asNoTracking)
		{
			_connectionString = connectionString;
			_asNoTracking = asNoTracking;
		}

		public AppDbContextFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		public IAppDbContext Create(bool ensureMigrated = false)
		{
			var context = new AppDbContext(_connectionString, _asNoTracking);

			if (ensureMigrated)
				context.Database.Migrate();

			return context;
		}
	}
}
