using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;

namespace Gems.ApplicationServices.Services
{
	public class DataImportService
	{
		private IAppDbContext _appDbContext;

		private List<Building> buildings;
		private List<RoadNetworkElement> roadNetworkElements;
		private List<PlaningStructureElement> planingStructureElements;
		private List<Settlement> settlements;
		private List<City> cities;
		private List<Region> regions;
		private List<Address> addresses;
		private List<Territory> territories;
		private List<MunicipalArea> municipalAreas;
		private List<AdministrativeArea> administrativeAreas;
		private List<DataSourceBase> dataSources;
		private List<DataSourceBase> attachedDataSource;

        public DataImportService(IAppDbContextFactory appDbContextFactory, ILogger logger)
		{
			_appDbContext = appDbContextFactory.Create(true);
			addresses = new List<Address>();
			buildings = new List<Building>();
			roadNetworkElements = new List<RoadNetworkElement>();
			cities = new List<City>();
			settlements = new List<Settlement>();
			regions = new List<Region>();
			territories = new List<Territory>();
			municipalAreas = new List<MunicipalArea>();
			administrativeAreas = new List<AdministrativeArea>();
			planingStructureElements = new List<PlaningStructureElement>();
			dataSources = new List<DataSourceBase>();
			attachedDataSource = new List<DataSourceBase>();

			GettingDataTromTheDatabase(logger);
		}

		public void ImportBuildingAsync(Building buildingImport, CancellationToken cancellationToken = default)
		{
			var externalBuildingId = buildingImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
			if (externalBuildingId != null)
			{
				var building = buildings.FirstOrDefault(o => o
								.DataSources
								.Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalBuildingId) is true);

                if (building != null)
				{
					DataImportHelper.Map(buildingImport, building);

                    _appDbContext.Update(building);
				}
                else
                    _appDbContext.Add(buildingImport);
            }
		}

		public void ImportRoadNetworkElementAsync(RoadNetworkElement roadNetworkElementImport, CancellationToken cancellationToken = default)
		{
			var externalRoadNetworkElementPlotId = roadNetworkElementImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
			if (externalRoadNetworkElementPlotId != null)
			{
				var roadNetworkElement = roadNetworkElements.FirstOrDefault(o => o
                                .DataSources
                                .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalRoadNetworkElementPlotId) is true);

                if (roadNetworkElement != null)
				{
					DataImportHelper.Map(roadNetworkElementImport, roadNetworkElement);

                    _appDbContext.Update(roadNetworkElement);
				}
                else
                    _appDbContext.Add(roadNetworkElementImport);
            }
		}

		public void ImportPlaningStructureElementAsync(PlaningStructureElement planingStructureElementImport, CancellationToken cancellationToken = default)
		{
			var externalPlaningStructureElementId = planingStructureElementImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
			if (externalPlaningStructureElementId != null)
			{
                var planingStructureElement = planingStructureElements.FirstOrDefault(o => o
								.DataSources
								.Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalPlaningStructureElementId) is true);
				if (planingStructureElement != null)
				{
					DataImportHelper.Map(planingStructureElementImport, planingStructureElement);

                    _appDbContext.Update(planingStructureElement);
				}
                else
                    _appDbContext.Add(planingStructureElementImport);
            }
		}

		public void ImportSettlementAsync(Settlement settlementImport, CancellationToken cancellationToken = default)
		{
			var externalSettlementId = settlementImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
			if (externalSettlementId != null)
			{
                var settlement = settlements.FirstOrDefault(o => o
								.DataSources
								.Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalSettlementId) is true);
				if (settlement != null)
				{
					DataImportHelper.Map(settlementImport, settlement);

                    _appDbContext.Update(settlement);
				}
                else
                    _appDbContext.Add(settlementImport);
            }
		}

		public void ImportCityAsync(City cityImport, CancellationToken cancellationToken = default)
		{
			var externalCityId = cityImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
			if (externalCityId != null)
			{
				var city = cities.FirstOrDefault(o => o
								.DataSources
								.Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalCityId) is true);
				if (city != null)
				{
					DataImportHelper.Map(cityImport, city);

                    _appDbContext.Update(city);
				}
                else
                    _appDbContext.Add(cityImport);
            }
		}

		public void ImportTerritoryAsync(Territory territoryImport, CancellationToken cancellationToken = default)
		{
			var externalTerritoryId = territoryImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
			if (externalTerritoryId != null)
			{
				var territory = territories.FirstOrDefault(o => o
								.DataSources
								.Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalTerritoryId) is true);
				if (territory != null)
				{
					DataImportHelper.Map(territoryImport, territory);

                    _appDbContext.Update(territory);
				}
                else
                    _appDbContext.Add(territoryImport);
            }
		}
		public  void ImportMunicipalAreaAsync(MunicipalArea municipalAreaImport, CancellationToken cancellationToken = default)
		{

			var externalMunicipalAreaId = municipalAreaImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
			if (externalMunicipalAreaId != null)
			{
				var municipalArea = municipalAreas.FirstOrDefault(o => o
								.DataSources
								.Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalMunicipalAreaId) is true);
				if (municipalArea != null)
				{
					DataImportHelper.Map(municipalAreaImport, municipalArea);

                    _appDbContext.Update(municipalArea);
				}
                else
                    _appDbContext.Add(municipalAreaImport);
            }
		}
		public void ImportAdministrativeAreaAsync(AdministrativeArea administrativeAreaImport, CancellationToken cancellationToken = default)
		{
			var externalAdministrativeAreaId = administrativeAreaImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
			if (externalAdministrativeAreaId != null)
			{
				var administrativeArea = administrativeAreas.FirstOrDefault(o => o
								.DataSources
								.Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalAdministrativeAreaId) is true);
				if (administrativeArea != null)
				{
					DataImportHelper.Map(administrativeAreaImport, administrativeArea);

                    _appDbContext.Update(administrativeArea);
				}
                else
                    _appDbContext.Add(administrativeAreaImport);
            }
		}
		public void ImportRegionAsync(Region regionImport, CancellationToken cancellationToken = default)
		{
			var externalRegionId = regionImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
			if (externalRegionId != null)
			{
                var region = regions.FirstOrDefault(o => o
								.DataSources
								.Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalRegionId) is true);
				if (region != null)
				{
					DataImportHelper.Map(regionImport, region);

                    _appDbContext.Update(region);
				}
                else
                    _appDbContext.Add(regionImport);
            }
		}

		public async Task<int> ImportAddressesAsync(IReadOnlyCollection<Address> addressesImport, ILogger logger, CancellationToken cancellationToken = default)
		{
			SetAttachToRepeatDataSources(addressesImport);

            float counter = 0;
			int interest = 0;

            logger.LogDebug($"Подготовлено к записи 0% адресов");

            foreach (Address addressImport in addressesImport)
			{
				counter++;
				if(counter/addressesImport.Count > 0.05)
				{
					counter = 0;
					interest += 5;
					logger.LogDebug($"Подготовлено к записи {interest}% адресов");
				}
                var externalAddressId = addressImport.Building?.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
				if (externalAddressId == null)
					continue;

				var address = addresses.FirstOrDefault(o => o
								.Building
								?.DataSources
								.Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalAddressId) is true);

				if (address != null)
				{
					if (address.Building != null)
						ImportBuildingAsync(addressImport.Building!, cancellationToken);
					if (address.RoadNetworkElement != null)
						ImportRoadNetworkElementAsync(addressImport.RoadNetworkElement!, cancellationToken);
					if (address.PlaningStructureElement != null)
						ImportPlaningStructureElementAsync(addressImport.PlaningStructureElement!, cancellationToken);
					if (address.Settlement != null)
						ImportSettlementAsync(addressImport.Settlement!, cancellationToken);
					if (address.City != null)
						ImportCityAsync(addressImport.City!, cancellationToken);
					if (address.Territory != null)
						ImportTerritoryAsync(addressImport.Territory!, cancellationToken);
					if (address.AdministrativeArea != null)
						ImportAdministrativeAreaAsync(addressImport.AdministrativeArea!, cancellationToken);
					if (address.MunicipalArea != null)
						ImportMunicipalAreaAsync(addressImport.MunicipalArea!, cancellationToken);
					if (address.Region != null)
						ImportRegionAsync(addressImport.Region, cancellationToken);

                    _appDbContext.Addresses.Update(address);
				}
				else
				{
					if (addressImport.IsCorrect())
                        _appDbContext.Addresses.Add(addressImport);
                    else
					{
						var invalidAddress = new InvalidAddress(addressImport);
						_appDbContext.InvalidAddresses.Add(invalidAddress);
					}
				}
			}
			try
			{
				logger.LogDebug("Подготовлено к записи 100% адресов");
				return await _appDbContext.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private void GettingDataTromTheDatabase(ILogger logger)
		{
            logger.LogDebug("Получение данных из базы");

            addresses = (_appDbContext
                            .Addresses
                            .ToList());
			logger.LogDebug("Получены все адреса из базы");

			dataSources = (_appDbContext
				.DataSource.AsNoTracking()
				.ToList());
			logger.LogDebug("Получены все источники данных из базы");

			buildings = _appDbContext.Buildings.ToList();
			logger.LogDebug("Получены все здания из базы");

			roadNetworkElements = _appDbContext.RoadNetworkElements.ToList();
			logger.LogDebug("Получены все улицы из базы");

			planingStructureElements = _appDbContext.PlaningStructureElements.ToList();
			logger.LogDebug("Получены все элементы планировочной структуры из базы");

			cities = _appDbContext.Cities.ToList();
			logger.LogDebug("Получены все города из базы");

			settlements = _appDbContext.Settlements.ToList();
			logger.LogDebug("Получены все села из базы");

			administrativeAreas = _appDbContext.AdministrativeAreas.ToList();
			logger.LogDebug("Получены все административные области из базы");

			territories = _appDbContext.Territories.ToList();
			logger.LogDebug("Получены все территории из базы");

			regions = _appDbContext.Regions.ToList();
			logger.LogDebug("Получены все регионы из базы");

			municipalAreas = _appDbContext.MunicipalAreas.ToList();
			logger.LogDebug("Получены все муниципальные районы из базы");

            logger.LogDebug("Данные из базы получены");
        }

		private void SetAttachToRepeatDataSources(IReadOnlyCollection<Address> addresses)
		{
			List<DataSourceBase> dataSourcesImport = new();

			foreach (Address address in addresses)
			{
				if (address != null)
				{
					if (address.DataSources.Count > 0)
						dataSourcesImport.AddRange(address.DataSources);
					if(address.Building != null)
						if(address.Building.DataSources!=null)
							dataSourcesImport.AddRange(address.Building.DataSources);
					if (address.City != null)
						if(address.City.DataSources!=null)
							dataSourcesImport.AddRange(address.City.DataSources);
					if (address.Settlement!=null)
						if(address.Settlement.DataSources!=null)
							dataSourcesImport.AddRange(address.Settlement.DataSources);
					if (address.RoadNetworkElement != null)
						if (address.RoadNetworkElement.DataSources != null)
							dataSourcesImport.AddRange(address.RoadNetworkElement.DataSources);
					if (address.PlaningStructureElement != null)
						if (address.PlaningStructureElement.DataSources != null)
							dataSourcesImport.AddRange(address.PlaningStructureElement.DataSources);
					if(address.AdministrativeArea != null)
						if(address.AdministrativeArea.DataSources != null)
							dataSourcesImport.AddRange (address.AdministrativeArea.DataSources);
					if (address.MunicipalArea != null)
						if (address.MunicipalArea.DataSources != null)
							dataSourcesImport.AddRange(address.MunicipalArea.DataSources);
					if (address.Territory != null)
						if (address.Territory.DataSources != null)
							dataSourcesImport.AddRange(address.Territory.DataSources);
					if(address.Region != null)
						if(address.Region.DataSources != null)
							dataSourcesImport.AddRange(address.Region.DataSources);
				}
			}

			foreach(var entity in dataSourcesImport)
			{
				if(dataSources.Any(o => o.Id == entity.Id))
                    _appDbContext.DataSource.Attach(entity);
            }
		}

    }
}
