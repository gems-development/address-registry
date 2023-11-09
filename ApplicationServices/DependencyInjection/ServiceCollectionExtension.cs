using ApplicationServices.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
           services
            .AddScoped<DataImportService>()
            //.AddMediatR(typeof(ServiceCollectionExtension));
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly);
            });
    }
}
