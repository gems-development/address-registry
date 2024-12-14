using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.AddressRegistry.Entities.Enums;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.DataMergeServices.Common;

namespace Gems.DataMergeServices.Services
{
	public static class DataMergeService
	{
		private static readonly Dictionary<string, House> NormalizedOsmAddresses = new Dictionary<string, House>();
		private static readonly Dictionary<string, Address> NormalizedFiasAddresses = new Dictionary<string, Address>();

		private static readonly Dictionary<string, BuildingDataSource> OsmBuildingDataSources =
			new Dictionary<string, BuildingDataSource>();

		private static readonly Dictionary<string, ErnDataSource> OsmErnDataSources =
			new Dictionary<string, ErnDataSource>();

		private static readonly Dictionary<string, CityDataSource> OsmCityDataSources =
			new Dictionary<string, CityDataSource>();

		private static readonly Dictionary<string, SettlementDataSource> OsmSettlementDataSources =
			new Dictionary<string, SettlementDataSource>();

		private static readonly Dictionary<string, MunicipalAreaDataSource> OsmMunAreaDataSources =
			new Dictionary<string, MunicipalAreaDataSource>();

		private static readonly Dictionary<string, RegionDataSource> OsmRegionDataSources =
			new Dictionary<string, RegionDataSource>();

		public static async Task MergeAddresses(
			IReadOnlyCollection<House> addressesOsm,
			IReadOnlyCollection<Address> addressesFias,
			ILogger logger)
		{
			var normalizeOsmAddressesTask = Task.Run(() =>
			{
				logger.LogDebug("OSM || Начата нормализация адресов");
				foreach (var addressOsm in addressesOsm)
				{
					var normalizedAddress = addressOsm.GetNormalizedAddress(logger);
					NormalizedOsmAddresses[normalizedAddress] = addressOsm;
				}
                logger.LogDebug("OSM || Нормализация адресов завершена");
            });
			var normalizeFiasAddressesTask = Task.Run(() =>
			{
				logger.LogDebug("ФИАС || Начата нормализация адресов");
                foreach (var addressFias in addressesFias)
				{
					var normalizedAddress = addressFias.GetNormalizedAddress(logger);
					NormalizedFiasAddresses[normalizedAddress] = addressFias;
				}
                logger.LogDebug("ФИАС || Нормализация адресов завершена");
            });

			await Task.WhenAll(
				normalizeOsmAddressesTask,
				normalizeFiasAddressesTask);

			SearchDuplicatesByUpdateDate(NormalizedFiasAddresses, logger);

			foreach (var normalizedAddress in NormalizedOsmAddresses.Keys)
			{
				if (NormalizedFiasAddresses.TryGetValue(normalizedAddress, out var correspondingFiasAddress))
				{
					var correspondingOsmAddress = NormalizedOsmAddresses[normalizedAddress];
					AddGeometryToAddress(correspondingFiasAddress, correspondingOsmAddress, logger);
				}
			}

			LogEfficiencies(logger);
        }

        private static void LogEfficiencies(ILogger logger)
        {
            try
            {
                var results = CalculateEfficiencies(NormalizedFiasAddresses, NormalizedOsmAddresses);

                using (var loggerScope = logger.BeginScope("Результативность программы"))
                {
                    logger.LogDebug("====== Начало блока результативности ======");
                    logger.LogDebug($"Кол-во адресов OSM: {results.CountOsmAddresses}");
                    logger.LogDebug($"Кол-во адресов FIAS: {results.CountFiasAddresses}");
                    logger.LogDebug($"Кол-во адресов с геометрией: {results.CountAddressesWithGeometry}");
                    logger.LogDebug($"Результативность алгоритма(Отражает качество, без учета качества исходных данных): {results.AlgorithmEfficiency:F2}%");
                    logger.LogDebug($"Результативность общая: {results.OverallEfficiency:F2}%");
                    logger.LogDebug($"Покрытие адресов FIAS, адресами из OSM: {results.OsmToFiasCoverage:F2}%");
                    logger.LogDebug("====== Конец блока результативности ======");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Произошла ошибка при вычислении результативности.");
            }
        }

        private static EfficiencyResults CalculateEfficiencies(
			Dictionary<string, Address> normalizedFiasAddresses,
			Dictionary<string, House> normalizedOsmAddresses)
        {
            int countOsmAddresses = normalizedOsmAddresses.Count;
            int countFiasAddresses = normalizedFiasAddresses.Count;

            if (countOsmAddresses == 0 || countFiasAddresses == 0)
            {
                throw new InvalidOperationException("Недостаточно данных для вычисления результативности: количество адресов OSM или FIAS равно нулю.");
            }

            int countAddressesWithGeometry = normalizedFiasAddresses.Count(o => o.Value?.Building?.GeoJson != null);

            double algorithmEfficiency = (double)countAddressesWithGeometry / countOsmAddresses * 100;
            double overallEfficiency = (double)countAddressesWithGeometry / countFiasAddresses * 100;
            double osmToFiasCoverage = (double)countOsmAddresses / countFiasAddresses * 100;

            return new EfficiencyResults(countOsmAddresses, countFiasAddresses, countAddressesWithGeometry, algorithmEfficiency, overallEfficiency, osmToFiasCoverage);
        }

        public static void SearchDuplicatesByUpdateDate(Dictionary<string, Address> NormalizedFiasAddresses, ILogger logger)
		{
			var keys = NormalizedFiasAddresses.Keys.ToList();

			for (var i = 0 ; i < NormalizedFiasAddresses.Count-1; i++) {
				for(var j = i+1 ; j < NormalizedFiasAddresses.Count; j++) {
					var normalizedAddress_1 = keys[i];
					var normalizedAddress_2 = keys[j];
					if (СheckForDuplicates(normalizedAddress_1, normalizedAddress_2))
					{
						var date1 = NormalizedFiasAddresses[normalizedAddress_1].Building.FiasDateUpdated;
						var date2 = NormalizedFiasAddresses[normalizedAddress_2].Building.FiasDateUpdated;
						if (DateTime.Compare(date1, date2) >= 0)
						{
							logger.LogTrace($"ФИАС || Найден дубликат адреса: {normalizedAddress_2} + дата обновления: {date2}");
						}
						else
						{
							logger.LogTrace($"ФИАС || Найден дубликат адреса: {normalizedAddress_1} + дата обновления: {date1}");
						}
						break;
					}			
				}	
			}
		}

		public static bool СheckForDuplicates(string normalizedAddress_1, string normalizedAddress_2) =>
			normalizedAddress_1.Equals(normalizedAddress_2);

		private static void AddGeometryToAddress(Address address, House house, ILogger logger)
		{
			var buildingDataSource = new BuildingDataSource();

			buildingDataSource.Building = address.Building;
			buildingDataSource.Id = house.Id.ToString();
			buildingDataSource.SourceType = SourceType.Osm;

			address.Building!.GeoJson = house.GeoJson;
			address.GeoJson = house.GeoJson;

			var foundDataSource = FindBuildingDataSource(buildingDataSource.Id);
			if (foundDataSource == null)
			{
				OsmBuildingDataSources.Add(buildingDataSource.Id, buildingDataSource);
				DataMergeHelper.TryAddOsmDataSource(address.Building, buildingDataSource, logger);
			}
			else
				DataMergeHelper.TryAddOsmDataSource(address.Building, (BuildingDataSource) foundDataSource, logger);

			var roadNetworkElementDataSource = new ErnDataSource();
			roadNetworkElementDataSource.Ern = address.RoadNetworkElement;
			roadNetworkElementDataSource.Id = house.Street.Id.ToString();
			roadNetworkElementDataSource.SourceType = SourceType.Osm;

			address.RoadNetworkElement!.GeoJson = house.Street!.GeoJson;
			foundDataSource = FindErnDataSource(roadNetworkElementDataSource.Id);
			if (foundDataSource == null)
			{
				OsmErnDataSources.Add(roadNetworkElementDataSource.Id, roadNetworkElementDataSource);
				DataMergeHelper.TryAddOsmDataSource(address.RoadNetworkElement, roadNetworkElementDataSource);
			}
			else
				DataMergeHelper.TryAddOsmDataSource(address.RoadNetworkElement, (ErnDataSource) foundDataSource);

			if (house.Street.City != null)
			{
				if (address.City != null)
				{
					var cityDataSource = new CityDataSource();
					cityDataSource.City = address.City;
					cityDataSource.Id = house.Street.City.Id.ToString();
					cityDataSource.SourceType = SourceType.Osm;

					address.City.GeoJson = house.Street.City.GeoJson;
					foundDataSource = FindCityDataSource(cityDataSource.Id);
					if (foundDataSource == null)
					{
						OsmCityDataSources.Add(cityDataSource.Id, cityDataSource);
						DataMergeHelper.TryAddOsmDataSource(address.City, cityDataSource, logger);
					}
					else
						DataMergeHelper.TryAddOsmDataSource(address.City, (CityDataSource) foundDataSource, logger);
				}
				else if (address.Settlement != null)
				{
					var settlementDataSource = new SettlementDataSource();
					settlementDataSource.Settlement = address.Settlement;
					settlementDataSource.Id = house.Street.City.Id.ToString();
					settlementDataSource.SourceType = SourceType.Osm;

					address.Settlement.GeoJson = house.Street.City.GeoJson;
					foundDataSource = FindSettlementDataSource(settlementDataSource.Id);
					if (foundDataSource == null)
					{
						OsmSettlementDataSources.Add(settlementDataSource.Id, settlementDataSource);
						DataMergeHelper.TryAddOsmDataSource(address.Settlement, settlementDataSource, logger);
					}
					else
						DataMergeHelper.TryAddOsmDataSource(address.Settlement, (SettlementDataSource) foundDataSource, logger);
				}
				else
				{
					logger.LogDebug($"объект {house.Street.City.Name} не найден в системе ФИАС");
					return;
				}

				var municipalAreaDataSource = new MunicipalAreaDataSource();
				municipalAreaDataSource.MunicipalArea = address.MunicipalArea;
				municipalAreaDataSource.Id = house.Street.City.District.Id.ToString();
				municipalAreaDataSource.SourceType = SourceType.Osm;

				address.MunicipalArea.GeoJson = house.Street.City.District.GeoJson;
				foundDataSource = FindMunAreaDataSource(municipalAreaDataSource.Id);
				if (foundDataSource == null)
				{
					OsmMunAreaDataSources.Add(municipalAreaDataSource.Id, municipalAreaDataSource);
					DataMergeHelper.TryAddOsmDataSource(address.MunicipalArea, municipalAreaDataSource, logger);
				}
				else
					DataMergeHelper.TryAddOsmDataSource(address.MunicipalArea, (MunicipalAreaDataSource) foundDataSource, logger);

				var regionDataSource = new RegionDataSource();
				regionDataSource.Region = address.Region;
				regionDataSource.Id = house.Street.City.District.Area.Id.ToString();
				regionDataSource.SourceType = SourceType.Osm;

				address.Region.GeoJson = house.Street.City.District.Area.GeoJson;
				foundDataSource = FindRegionDataSource(regionDataSource.Id);
				if (foundDataSource == null)
				{
					OsmRegionDataSources.Add(regionDataSource.Id, regionDataSource);
					DataMergeHelper.TryAddOsmDataSource(address.Region, regionDataSource, logger);
				}
				else
					DataMergeHelper.TryAddOsmDataSource(address.Region, (RegionDataSource) foundDataSource, logger);
			}
			else if (house.Street.Village != null)
			{
				if (address.Settlement != null)
				{
					var settlementDataSource = new SettlementDataSource();
					settlementDataSource.Settlement = address.Settlement;
					settlementDataSource.Id = house.Street.Village.Id.ToString();
					settlementDataSource.SourceType = SourceType.Osm;

					address.Settlement.GeoJson = house.Street.Village.GeoJson;
					foundDataSource = FindSettlementDataSource(settlementDataSource.Id);
					if (foundDataSource == null)
					{
						OsmSettlementDataSources.Add(settlementDataSource.Id, settlementDataSource);
						DataMergeHelper.TryAddOsmDataSource(address.Settlement, settlementDataSource, logger);
					}
					else
						DataMergeHelper.TryAddOsmDataSource(address.Settlement, (SettlementDataSource) foundDataSource, logger);
				}
				else if (address.City != null)
				{
					var cityDataSource = new CityDataSource();
					cityDataSource.City = address.City;
					cityDataSource.Id = house.Street.Village.Id.ToString();
					cityDataSource.SourceType = SourceType.Osm;

					address.City.GeoJson = house.Street.Village.GeoJson;
					foundDataSource = FindCityDataSource(cityDataSource.Id);
					if (foundDataSource == null)
					{
						OsmCityDataSources.Add(cityDataSource.Id, cityDataSource);
						DataMergeHelper.TryAddOsmDataSource(address.City, cityDataSource, logger);
					}
					else
						DataMergeHelper.TryAddOsmDataSource(address.City, (CityDataSource) foundDataSource, logger);
				}
				else
				{
					logger.LogDebug($"объект {house.Street.Village.Name} не найден в системе ФИАС");
					return;
				}

				var municipalAreaDataSource = new MunicipalAreaDataSource();
				municipalAreaDataSource.MunicipalArea = address.MunicipalArea;
				municipalAreaDataSource.Id = house.Street.Village.District.Id.ToString();
				municipalAreaDataSource.SourceType = SourceType.Osm;

				address.MunicipalArea.GeoJson = house.Street.Village.District.GeoJson;
				foundDataSource = FindMunAreaDataSource(municipalAreaDataSource.Id);
				if (foundDataSource == null)
				{
					OsmMunAreaDataSources.Add(municipalAreaDataSource.Id, municipalAreaDataSource);
					DataMergeHelper.TryAddOsmDataSource(address.MunicipalArea, municipalAreaDataSource, logger);
				}
				else
					DataMergeHelper.TryAddOsmDataSource(address.MunicipalArea, (MunicipalAreaDataSource) foundDataSource, logger);

				var regionDataSource = new RegionDataSource();
				regionDataSource.Region = address.Region;
				regionDataSource.Id = house.Street.Village.District.Area.Id.ToString();
				regionDataSource.SourceType = SourceType.Osm;

				address.Region.GeoJson = house.Street.Village.District.Area.GeoJson;
				foundDataSource = FindRegionDataSource(regionDataSource.Id);
				if (foundDataSource == null)
				{
					OsmRegionDataSources.Add(regionDataSource.Id, regionDataSource);
					DataMergeHelper.TryAddOsmDataSource(address.Region, regionDataSource, logger);
				}
				else
					DataMergeHelper.TryAddOsmDataSource(address.Region, (RegionDataSource) foundDataSource, logger);
			}
		}

		private static DataSourceBase? FindBuildingDataSource(string id) =>
			OsmBuildingDataSources.FirstOrDefault(o => o.Key == id).Value;

		private static DataSourceBase? FindErnDataSource(string id) =>
			OsmErnDataSources.FirstOrDefault(o => o.Key == id).Value;

		private static DataSourceBase? FindCityDataSource(string id) =>
			OsmCityDataSources.FirstOrDefault(o => o.Key == id).Value;

		private static DataSourceBase? FindSettlementDataSource(string id) =>
			OsmSettlementDataSources.FirstOrDefault(o => o.Key == id).Value;

		private static DataSourceBase? FindMunAreaDataSource(string id) =>
			OsmMunAreaDataSources.FirstOrDefault(o => o.Key == id).Value;

		private static DataSourceBase? FindRegionDataSource(string id) =>
			OsmRegionDataSources.FirstOrDefault(o => o.Key == id).Value;
	}
}
