using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;

namespace Gems.ApplicationServices.Services
{
    public class DataImportService
    {
        private IAppDbContext _AppDbContext;
        public DataImportService(IAppDbContextFactory appDbContextFactory)
        {
            _AppDbContext = appDbContextFactory.Create();
        }

        public Task SpaceImportAsync(IReadOnlyCollection<Space> spacesImport, CancellationToken cancellationToken = default)
        {
            foreach (Space SpaceImport in spacesImport)
            {
                if (_AppDbContext.Spaces.Any(Space => Space.Id == SpaceImport.Id))
                    _AppDbContext.Update(SpaceImport);
                else
                    _AppDbContext.Add(SpaceImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task BuildingImportAsync(IReadOnlyCollection<Building> BuildingsImport, CancellationToken cancellationToken = default)
        {
            foreach (Building BuildingImport in BuildingsImport)
            {
                if (_AppDbContext.Buildings.Any(Building => Building.Id == BuildingImport.Id))
                    _AppDbContext.Update(BuildingImport);
                else
                    _AppDbContext.Add(BuildingImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task LandPlotImportAsync(IReadOnlyCollection<LandPlot> LandPlotsImport, CancellationToken cancellationToken = default)
        {
            foreach (LandPlot LandPlotImport in LandPlotsImport)
            {
                if (_AppDbContext.LandPlots.Any(LandPlot => LandPlot.Id == LandPlotImport.Id))
                    _AppDbContext.Update(LandPlotImport);
                else
                    _AppDbContext.Add(LandPlotImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task RoadNetworkElementImportAsync(IReadOnlyCollection<RoadNetworkElement> RoadNetworkElementsImport, CancellationToken cancellationToken = default)
        {
            foreach (RoadNetworkElement RoadNetworkElementImport in RoadNetworkElementsImport)
            {
                if (_AppDbContext.RoadNetworkElements.Any(RoadNetworkElement => RoadNetworkElement.Id == RoadNetworkElementImport.Id))
                    _AppDbContext.Update(RoadNetworkElementImport);
                else
                    _AppDbContext.Add(RoadNetworkElementImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task PlaningStructureElementImportAsync(IReadOnlyCollection<PlaningStructureElement> PlaningStructureElementsImport, CancellationToken cancellationToken = default)
        {
            foreach (PlaningStructureElement PlaningStructureElementImport in PlaningStructureElementsImport)
            {
                if (_AppDbContext.PlaningStructureElements.Any(PlaningStructureElement => PlaningStructureElement.Id == PlaningStructureElementImport.Id))
                    _AppDbContext.Update(PlaningStructureElementImport);
                else
                    _AppDbContext.Add(PlaningStructureElementImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task SettlementImportAsync(IReadOnlyCollection<Settlement> SettlementsImport, CancellationToken cancellationToken = default)
        {
            foreach (Settlement SettlementImport in SettlementsImport)
            {
                if (_AppDbContext.Settlements.Any(Settlement => Settlement.Id == SettlementImport.Id))
                    _AppDbContext.Update(SettlementImport);
                else
                    _AppDbContext.Add(SettlementImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task CityImportsAsync(IReadOnlyCollection<City> CitiesImport, CancellationToken cancellationToken = default)
        {
            foreach (City CityImport in CitiesImport)
            {
                if (_AppDbContext.Cities.Any(City => City.Id == CityImport.Id))
                    _AppDbContext.Update(CityImport);
                else
                    _AppDbContext.Add(CityImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task TerritoryImportAsync(IReadOnlyCollection<Territory> TerritoriesImport, CancellationToken cancellationToken = default)
        {
            foreach (Territory TerrytoryImport in TerritoriesImport)
            {
                if (_AppDbContext.Territories.Any(Territory => Territory.Id == TerrytoryImport.Id))
                    _AppDbContext.Update(TerrytoryImport);
                else
                    _AppDbContext.Add(TerrytoryImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task MunicipalAreaImportAsync(IReadOnlyCollection<MunicipalArea> MunicipalAreasImport, CancellationToken cancellationToken = default)
        {
            foreach (MunicipalArea MunicipalAreaImport in MunicipalAreasImport)
            {
                if (_AppDbContext.MunicipalAreas.Any(MunicipalArea => MunicipalArea.Id == MunicipalAreaImport.Id))
                    _AppDbContext.Update(MunicipalAreaImport);
                else
                    _AppDbContext.Add(MunicipalAreaImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task AdministrativeAreaImportAsync(IReadOnlyCollection<AdministrativeArea> AdministrativeAreasImport, CancellationToken cancellationToken = default)
        {
            foreach (AdministrativeArea AdministrativeAreaImport in AdministrativeAreasImport)
            {
                if (_AppDbContext.AdministrativeAreas.Any(AdministrativeArea => AdministrativeArea.Id == AdministrativeAreaImport.Id))
                    _AppDbContext.Update(AdministrativeAreaImport);
                else
                    _AppDbContext.Add(AdministrativeAreaImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task RegionImportAsync(IReadOnlyCollection<Region> RegionsImport, CancellationToken cancellationToken = default)
        {
            foreach (Region RegionImport in RegionsImport)
            {
                if (_AppDbContext.Regions.Any(Region => Region.Id == RegionImport.Id))
                    _AppDbContext.Update(RegionImport);
                else
                    _AppDbContext.Add(RegionImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task CountryImportAsync(IReadOnlyCollection<Country> CountriesImport, CancellationToken cancellationToken = default)
        {
            foreach (Country CountryImport in CountriesImport)
            {
                if (_AppDbContext.Countries.Any(Country => Country.Id == CountryImport.Id))
                    _AppDbContext.Update(CountryImport);
                else
                    _AppDbContext.Add(CountryImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);
        }
        public Task AddressImportAsync(IReadOnlyCollection<Address> AddressesImport, CancellationToken cancellationToken = default)
        {
            foreach (Address AddressImport in AddressesImport)
            {
                if (_AppDbContext.Addresses.Any(Address => Address.Id == AddressImport.Id))
                    _AppDbContext.Update(AddressImport);
                else
                    _AppDbContext.Add(AddressImport);
            }
            return _AppDbContext.SaveChangesAsync(cancellationToken);

        }



    }
}