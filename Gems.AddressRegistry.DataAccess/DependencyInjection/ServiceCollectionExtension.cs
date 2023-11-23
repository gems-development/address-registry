using Gems.AddressRegistry.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services) =>
           services
            .AddSingleton<IAppDbContextFactory, AppDbContextFactory>(serviceProvider =>
            {
                var connectionString = serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
                return new AppDbContextFactory(connectionString);
            });

    }

}
