using System.Diagnostics;
using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.OsmDataParser;
using Gems.ApplicationServices.Services;
using Gems.DataMergeServices.Services;
using Microsoft.Extensions.Configuration;

namespace Gems.AddressRegistry.OsmClient;

public static class Client
{
    private static readonly IConfiguration Configuration;

    static Client()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;

        Configuration = new ConfigurationBuilder()
            .SetBasePath(projectDirectory!)
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public static async Task Main(string[] args)
    {
        var sw = Stopwatch.StartNew();
        var fiasConverter = new FiasXmlToEntityConverter();
        var connectionString = Configuration.GetSection("ConnectionString").Value;
        var pathToPbf = Configuration.GetSection("Pbf File").Value;
        var areaName = Configuration.GetSection("Target Area").Value;
        var pathAdm = Configuration.GetSection("PathAdm").Value;
        var pathMun = Configuration.GetSection("PathMun").Value;
        var pathReg = Configuration.GetSection("PathReg").Value;
        var pathBuildings = Configuration.GetSection("PathBuildings").Value;

        var osmTask = Task.Run(() => OsmParserTask.Execute(pathToPbf!, areaName!));
        var fiasTask = Task.Run(() => fiasConverter.ReadAndBuildAddresses(pathAdm!, pathMun!, pathReg!, pathBuildings!));
        var tasks = new Task[]
        {
            osmTask,
            fiasTask
        };

        await Task.WhenAll(tasks).ContinueWith(task
            => DataMergeService.MergeAddresses(osmTask.Result, fiasTask.Result));

        IAppDbContextFactory appDbContextFactory = new AppDbContextFactory(connectionString!);
        DataImportService dataImportService = new DataImportService(appDbContextFactory);

        await dataImportService.ImportAddressesAsync(fiasTask.Result);

        sw.Stop();
        Debug.WriteLine(sw.Elapsed);
    }
}