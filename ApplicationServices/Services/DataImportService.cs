using Gems.AddressRegistry.DataAccess;
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
                    DataImportHelper.Map(roadNetworkElementImport, roadNetworkElement);

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
                    DataImportHelper.Map(planingStructureElementImport, planingStructureElement);


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
                    DataImportHelper.Map(settlementImport, settlement);


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
                    DataImportHelper.Map(cityImport, city);

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
                    DataImportHelper.Map(territoryImport, territory);
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
                    DataImportHelper.Map(municipalAreaImport, municipalArea);


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
                    DataImportHelper.Map(administrativeAreaImport, administrativeArea);


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
                    DataImportHelper.Map(regionImport, region);
                    _appDbContext.Update(region);
                }
                else
                    _appDbContext.Add(regionImport);
            }
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> ImportAddressesAsync(IReadOnlyCollection<Address> addressesImport, CancellationToken cancellationToken = default)
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
                    if (address.Building != null)
                        await ImportBuildingAsync(addressImport.Building!, cancellationToken);
                    if (address.RoadNetworkElement != null)
                        await ImportRoadNetworkElementAsync(addressImport.RoadNetworkElement!, cancellationToken);
                    if (address.PlaningStructureElement != null)
                        await ImportPlaningStructureElementAsync(addressImport.PlaningStructureElement!, cancellationToken);
                    if (address.Settlement != null)
                        await ImportSettlementAsync(addressImport.Settlement!, cancellationToken);
                    if (address.City != null)
                        await ImportCityAsync(addressImport.City!, cancellationToken);
                    if (address.Territory != null)
                        await ImportTerritoryAsync(addressImport.Territory!, cancellationToken);
                    if (address.AdministrativeArea != null)
                        await ImportAdministrativeAreaAsync(addressImport.AdministrativeArea!, cancellationToken);
                    if (address.MunicipalArea != null)
                        await ImportMunicipalAreaAsync(addressImport.MunicipalArea!, cancellationToken);
                    if (address.Region != null)
                        await ImportRegionAsync(addressImport.Region, cancellationToken);

                    _appDbContext.Addresses.Update(address);
                }
                else

                if (addressImport.IsCorrect())
                {
                    _appDbContext.Addresses.Add(addressImport);
                }
                else
                {
                    var invalidAddress = new InvalidAddress(addressImport);
                     _appDbContext.InvalidAddresses.Add(invalidAddress);
                }
            }
            try
            {
                return await _appDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }

        }



    }
}