using Gems.ApplicationServices.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServicesServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
           services
            .AddScoped<DataImportService>()
            //.AddMediatR(typeof(ServiceCollectionExtension));
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationServicesServiceCollectionExtension).Assembly);
            });
    }
}
