using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.AddressRegistry.Entities.Enums;
using Gems.AddressRegistry.OsmDataParser.Model;

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

			foreach (var normalizedAddress in NormalizedOsmAddresses.Keys)
			{
				if (NormalizedFiasAddresses.TryGetValue(normalizedAddress, out var correspondingFiasAddress))
				{
					var correspondingOsmAddress = NormalizedOsmAddresses[normalizedAddress];
					AddGeometryToAddress(correspondingFiasAddress, correspondingOsmAddress, logger);
				}
			}
		}

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
