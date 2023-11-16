using Gems.AddressRegistry.DataAccess;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services) =>
           services
            .AddScoped<IAppDbContextFactory, AppDbContextFactory>();
    }
}
