using Serilog;

namespace WebApi.Helpers
{
    public static class SerilogServices
    {
        public static IServiceCollection AddSerilogServices(
        this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("/var/log/addressRegistryService.log")
            .CreateLogger();

            return services.AddSingleton(Log.Logger);
        }

    }
}
