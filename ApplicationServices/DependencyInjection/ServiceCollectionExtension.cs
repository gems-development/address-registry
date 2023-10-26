
using Gems.AddressRegistry.ApplicationServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
           services
            .AddScoped<DataImportService>();
    }
}
