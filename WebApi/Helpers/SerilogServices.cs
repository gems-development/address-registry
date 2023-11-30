using Serilog;
using Serilog.Events;

namespace WebApi.Helpers
{
    public static class SerilogServices
    {
        public static IServiceCollection AddSerilogServices(
        this IServiceCollection services)
        {
            var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("/var/log/addressRegistryService.log", restrictedToMinimumLevel: LogEventLevel.Warning)
            .WriteTo.Console()
            .CreateLogger();

            return services.AddSingleton<Serilog.ILogger>(logger);
        }

    }
}
