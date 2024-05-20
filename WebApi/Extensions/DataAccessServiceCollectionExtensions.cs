using Gems.AddressRegistry.DataAccess;

namespace Microsoft.Extensions.DependencyInjection;

public static class DataAccessServiceCollectionExtensions
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString, bool asNoTracking = false)
	{
		return services.AddSingleton<IAppDbContextFactory>(provider => new AppDbContextFactory(connectionString, asNoTracking));
	}
}
