using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.OsmClient.Helpers;
using Gems.AddressRegistry.OsmClient.Options;
using Gems.AddressRegistry.OsmDataParser;
using Gems.ApplicationServices.Services;
using Gems.DataMergeServices.Services;
using Serilog;

namespace Gems.AddressRegistry.OsmClient;

public static class Client
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
#if DEBUG
            .AddJsonFile("appsettings.Development.json")
#endif
            .Build();
        var swTotal = Stopwatch.StartNew();
        var serilog = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        var loggerFactory = new LoggerFactory().AddSerilog(serilog);
        var logger = loggerFactory.CreateLogger("Logger");

        try
        {
            var fiasConverter = new FiasXmlToEntityConverter();
            var connectionString = configuration.GetConnectionString("Default")!;
            var osmOptions = new OsmOptions();
            var fiasOptions = new FiasOptions();

            configuration.GetSection("Osm").Bind(osmOptions);
            configuration.GetSection("Fias").Bind(fiasOptions);

            var pathToPbf = InputFileNameHelper.GetFilePathByExtension(osmOptions.PbfFile);
            var areaName = osmOptions.TargetArea;
            var pathAdm = InputFileNameHelper.GetFilePathByNamePrefix(fiasOptions.PathAdm);
            var pathMun = InputFileNameHelper.GetFilePathByNamePrefix(fiasOptions.PathMun);
            var pathReg = InputFileNameHelper.GetFilePathByNamePrefix(fiasOptions.PathReg);
            var pathBuildings = InputFileNameHelper.GetFilePathByNamePrefix(fiasOptions.PathBuildings);
            var osmTask = Task.Run(async () =>
            {
                logger.LogInformation("Начат импорт OSM");

                var sw = Stopwatch.StartNew();
                var result = await OsmParserTask.Execute(pathToPbf, areaName, logger);


                logger.LogInformation($"Завершён импорт OSM: {sw.Elapsed}");

                return result;
            });
            var fiasTask = Task.Run(async () =>
            {
                logger.LogInformation("Начат импорт ФИАС");

                var sw = Stopwatch.StartNew();
                var result = await fiasConverter.ReadAndBuildAddresses(pathAdm, pathMun, pathBuildings, pathReg, logger as Microsoft.Extensions.Logging.ILogger);

                logger.LogInformation($"Завершён импорт ФИАС: {sw.Elapsed}");

                return result;
            });

            await Task.WhenAll(osmTask, fiasTask);
            logger.LogInformation("Начато слияние адресов");

            var sw = Stopwatch.StartNew();

            await DataMergeService.MergeAddresses(osmTask.Result, fiasTask.Result, logger);
            logger.LogInformation($"Слияние адресов завершено: {sw.Elapsed}");

            var appDbContextFactory = new AppDbContextFactory(connectionString);
            var dataImportService = new DataImportService(appDbContextFactory, logger);
            logger.LogInformation("Начат импорт адресов в БД");

            sw = Stopwatch.StartNew();

            await dataImportService.ImportAddressesAsync(fiasTask.Result, logger: logger);
            logger.LogInformation($"Импорт адресов в БД завершён: {sw.Elapsed}");
            logger.LogInformation($"Общая продолжительность: {swTotal.Elapsed}");
        }
        catch (Exception ex)
        {
            logger.LogError($"Исключение: {swTotal.Elapsed}");
            logger.LogError(ex.Message);
            logger.LogError(ex.StackTrace);
        }
    }
}
