using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.OsmClient.Helpers;
using Gems.AddressRegistry.OsmClient.Options;
using Gems.AddressRegistry.OsmDataParser;
using Gems.ApplicationServices.Services;
using Gems.DataMergeServices.Services;

namespace Gems.AddressRegistry.OsmClient;

public static class Client
{
	public static async Task Main(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();
		var swTotal = Stopwatch.StartNew();

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
				Console.WriteLine(">>> Начат импорт OSM");

				var sw = Stopwatch.StartNew();
				var result = await OsmParserTask.Execute(pathToPbf, areaName);

				Console.WriteLine($">>> Завершён импорт OSM: {sw.Elapsed}");

				return result;
			});
			var fiasTask = Task.Run(async () =>
			{
				Console.WriteLine(">>> Начат импорт ФИАС");

				var sw = Stopwatch.StartNew();
				var result = await fiasConverter.ReadAndBuildAddresses(pathAdm, pathMun, pathBuildings, pathReg);

				Console.WriteLine($">>> Завершён импорт ФИАС: {sw.Elapsed}");

				return result;
			});

			await Task.WhenAll(osmTask, fiasTask);
			Console.WriteLine(">>> Начато слияние адресов");

			var sw = Stopwatch.StartNew();

			await DataMergeService.MergeAddresses(osmTask.Result, fiasTask.Result);
			Console.WriteLine($">>> Слияние адресов завершено: {sw.Elapsed}");

			var appDbContextFactory = new AppDbContextFactory(connectionString);
			var dataImportService = new DataImportService(appDbContextFactory);

			sw = Stopwatch.StartNew();

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
