using Gems.AddressRegistry.DataAccess;

namespace Microsoft.Extensions.DependencyInjection;

public static class DataAccessServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, bool asNoTracking = false)
    {
        return services.AddSingleton<IAppDbContextFactory>(provider => new AppDbContextFactory(asNoTracking));
    }
}
