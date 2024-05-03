using Gems.ApplicationServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gems.ApplicationServices.DependencyInjection
{
    public static class ApplicationServicesServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services
                .AddScoped<DataImportService>();
    }
}
