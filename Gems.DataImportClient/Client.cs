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
		var swTotal = Stopwatch.StartNew();

		try
		{
			var fiasConverter = new FiasXmlToEntityConverter();
			var connectionString = Configuration.GetSection("ConnectionString").Value;
			var pathToPbf = Configuration.GetSection("Pbf File").Value;
			var areaName = Configuration.GetSection("Target Area").Value;
			var pathAdm = Configuration.GetSection("PathAdm").Value;
			var pathMun = Configuration.GetSection("PathMun").Value;
			var pathReg = Configuration.GetSection("PathReg").Value;
			var pathBuildings = Configuration.GetSection("PathBuildings").Value;

			var osmTask = Task.Run(async () =>
			{
				var sw = Stopwatch.StartNew();
				var result = await OsmParserTask.Execute(pathToPbf!, areaName!);

				Console.WriteLine($">>> Завершён импорт OSM: {sw.Elapsed}");

				return result;
			});
			var fiasTask = Task.Run(async () =>
			{
				var sw = Stopwatch.StartNew();
				var result = await fiasConverter.ReadAndBuildAddresses(pathAdm!, pathMun!, pathBuildings!, pathReg!);

				Console.WriteLine($">>> Завершён импорт ФИАС: {sw.Elapsed}");

				return result;
			});
			var tasks = new Task[]
			{
				osmTask,
				fiasTask
			};

			await Task
					.WhenAll(tasks)
					.ContinueWith(task =>
					{
						var sw = Stopwatch.StartNew();

						DataMergeService.MergeAddresses(osmTask.Result, fiasTask.Result);
						Console.WriteLine($">>> Слияние адресов завершено: {sw.Elapsed}");
					});

			var appDbContextFactory = new AppDbContextFactory(connectionString!);
			var dataImportService = new DataImportService(appDbContextFactory);
			var sw = Stopwatch.StartNew();

			await dataImportService.ImportAddressesAsync(fiasTask.Result);
			Console.WriteLine($">>> Импорт адресов в БД завершён: {sw.Elapsed}");
			Console.WriteLine($">>> Общая продолжительность: {swTotal.Elapsed}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($">>> Исключение: {swTotal.Elapsed}");
			Console.WriteLine(ex.Message);
			Console.WriteLine(ex.StackTrace);
		}
	}
}
