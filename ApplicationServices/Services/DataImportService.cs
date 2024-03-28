﻿using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gems.ApplicationServices.Services
{
    public class DataImportService
    {
        private IAppDbContext _appDbContext;
        public DataImportService(IAppDbContextFactory appDbContextFactory)
        {
            _appDbContext = appDbContextFactory.Create();
        }

        public async Task<int> ImportBuildingAsync(Building buildingImport, CancellationToken cancellationToken = default)
        {
            
                var externalBuildingId = buildingImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
            if (externalBuildingId != null)
            {
                var building = (await _appDbContext
                    .Buildings
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
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
            
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ImportRoadNetworkElementAsync(RoadNetworkElement roadNetworkElementImport, CancellationToken cancellationToken = default)
        {
            
                var externalRoadNetworkElementPlotId = roadNetworkElementImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
                if (externalRoadNetworkElementPlotId != null)
            {
                var roadNetworkElement = (await _appDbContext
                    .RoadNetworkElements
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
                        .DataSources
                        .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalRoadNetworkElementPlotId) is true);
                if (roadNetworkElement != null)
                {
                    // Обновление симантики
                    roadNetworkElement.Name = roadNetworkElementImport.Name;
                    roadNetworkElement.RoadNetworkElementType = roadNetworkElementImport.RoadNetworkElementType;

                    _appDbContext.Update(roadNetworkElement);
                }
                else
                    _appDbContext.Add(roadNetworkElementImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ImportPlaningStructureElementAsync(PlaningStructureElement planingStructureElementImport, CancellationToken cancellationToken = default)
        {
            
                var externalPlaningStructureElementId = planingStructureElementImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
                if (externalPlaningStructureElementId != null)
            {
                var planingStructureElement = (await _appDbContext
                    .PlaningStructureElements
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
                        .DataSources
                        .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalPlaningStructureElementId) is true);
                if (planingStructureElement != null)
                {
                    // Обновление симантики
                    planingStructureElement.Name = planingStructureElementImport.Name;


                    _appDbContext.Update(planingStructureElement);
                }
                else
                    _appDbContext.Add(planingStructureElementImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ImportSettlementAsync(Settlement settlementImport, CancellationToken cancellationToken = default)
        {
            
                var externalSettlementId = settlementImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
                if (externalSettlementId != null)
            {
                var settlement = (await _appDbContext
                    .Settlements
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
                        .DataSources
                        .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalSettlementId) is true);
                if (settlement != null)
                {
                    // Обновление симантики
                    settlement.Name = settlementImport.Name;


                    _appDbContext.Update(settlement);
                }
                else
                    _appDbContext.Add(settlementImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ImportCityAsync(City cityImport, CancellationToken cancellationToken = default)
        {
            
                var externalCityId = cityImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
                if (externalCityId != null)
            {
                var city = (await _appDbContext
                    .Cities
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
                        .DataSources
                        .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalCityId) is true);
                if (city != null)
                {
                    // Обновление симантики
                    city.Name = cityImport.Name;


                    _appDbContext.Update(city);
                }
                else
                    _appDbContext.Add(cityImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ImportTerritoryAsync(Territory territoryImport, CancellationToken cancellationToken = default)
        {
            
                var externalTerritoryId = territoryImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
                if (externalTerritoryId != null)
            { 
                var territory = (await _appDbContext
                    .Territories
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
                        .DataSources
                        .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalTerritoryId) is true);
                if (territory != null)
                {
                    // Обновление симантики
                    territory.Name = territoryImport.Name;


                    _appDbContext.Update(territory);
                }
                else
                    _appDbContext.Add(territoryImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ImportMunicipalAreaAsync(MunicipalArea municipalAreaImport, CancellationToken cancellationToken = default)
        {
            
                var externalMunicipalAreaId = municipalAreaImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
                if (externalMunicipalAreaId != null)
            {
                var municipalArea = (await _appDbContext
                    .MunicipalAreas
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
                        .DataSources
                        .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalMunicipalAreaId) is true);
                if (municipalArea != null)
                {
                    // Обновление симантики
                    municipalArea.Name = municipalAreaImport.Name;


                    _appDbContext.Update(municipalArea);
                }
                else
                    _appDbContext.Add(municipalAreaImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ImportAdministrativeAreaAsync(AdministrativeArea administrativeAreaImport, CancellationToken cancellationToken = default)
        {
            
                var externalAdministrativeAreaId = administrativeAreaImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
                if (externalAdministrativeAreaId != null)
            {
                var administrativeArea = (await _appDbContext
                    .AdministrativeAreas
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
                        .DataSources
                        .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalAdministrativeAreaId) is true);
                if (administrativeArea != null)
                {
                    // Обновление симантики
                    administrativeArea.Name = administrativeAreaImport.Name;


                    _appDbContext.Update(administrativeArea);
                }
                else
                    _appDbContext.Add(administrativeAreaImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ImportRegionAsync(Region regionImport, CancellationToken cancellationToken = default)
        {
            
                var externalRegionId = regionImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
                if (externalRegionId != null)
            {
                var region = (await _appDbContext
                    .Regions
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
                        .DataSources
                        .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalRegionId) is true);
                if (region != null)
                {
                    // Обновление симантики
                    region.Name = regionImport.Name;


                    _appDbContext.Update(region);
                }
                else
                    _appDbContext.Add(regionImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ImportCountryAsync(Country countryImport, CancellationToken cancellationToken = default)
        {
            
                var externalCountryId = countryImport.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
                if (externalCountryId != null)
            {
                var country = (await _appDbContext
                    .Countries
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
                        .DataSources
                        .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalCountryId) is true);
                if (country != null)
                {
                    // Обновление симантики
                    country.Name = countryImport.Name;


                    _appDbContext.Update(country);
                }
                else
                    _appDbContext.Add(countryImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> ImportAddressAsync(IReadOnlyCollection<Address> addressesImport, CancellationToken cancellationToken = default)
        {
            foreach (Address addressImport in addressesImport)
            {
                var externalAddressId = addressImport.Building?.DataSources.First(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias).Id;
                if (externalAddressId == null)
                    continue;
                // TODO переделать запрос
                var address = (await _appDbContext
                    .Addresses
                    .ToArrayAsync())
                    .FirstOrDefault(o => o
                        .Building
                        ?.DataSources
                        .Any(p => p.SourceType == AddressRegistry.Entities.Enums.SourceType.Fias && p.Id == externalAddressId) is true);
                if (address != null)
                {
                    // TODO перенести из addressImport данные в address
                    // При обновлении данных нужно искать есть ли этот объект в бд, если есть то обновлять его симантику
                    // если отсутствует, то добавлять запись о нём. Для этого нужно переписать функции импорта для сущностей выше
                    // Важно не допустить дублирования данных в базе
                    if(address.Building != null)
                        await ImportBuildingAsync(addressImport.Building, cancellationToken);
                    if (address.RoadNetworkElement != null)
                        await ImportRoadNetworkElementAsync(addressImport.RoadNetworkElement, cancellationToken);
                    if (address.PlaningStructureElement != null)
                        await ImportPlaningStructureElementAsync(addressImport.PlaningStructureElement, cancellationToken);
                    if (address.Settlement != null)
                        await ImportSettlementAsync(addressImport.Settlement, cancellationToken);
                    if (address.City != null)
                        await ImportCityAsync(addressImport.City, cancellationToken);
                    if (address.Territory != null)
                        await ImportTerritoryAsync(addressImport.Territory, cancellationToken);
                    if (address.AdministrativeArea != null)
                        await ImportAdministrativeAreaAsync(addressImport.AdministrativeArea, cancellationToken);
                    if (address.MunicipalArea != null)
                        await ImportMunicipalAreaAsync(addressImport.MunicipalArea, cancellationToken);
                    if (address.Region != null)
                        await ImportRegionAsync(addressImport.Region, cancellationToken);

                    _appDbContext.Update(address);
                }
                else
                    _appDbContext.Add(addressImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);

        }



    }
}