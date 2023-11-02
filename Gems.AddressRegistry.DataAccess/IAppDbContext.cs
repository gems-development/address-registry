using Gems.AddressRegistry.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Gems.AddressRegistry.DataAccess
{
    public interface IAppDbContext
    {
        DbSet<Address> Addresses { get; }
        DbSet<AdministrativeArea> AdministrativeAreas { get; }
        DbSet<Building> Buildings { get; }
        DbSet<City> Cities { get; }
        DbSet<Country> Countries { get; }
        DbSet<LandPlot> LandPlots { get; }
        DbSet<MunicipalArea> MunicipalAreas { get; }
        DbSet<PlaningStructureElement> PlaningStructureElements { get; }
        DbSet<Region> Regions { get; }
        DbSet<RoadNetworkElement> RoadNetworkElements { get; }
        DbSet<Settlement> Settlements { get; }
        DbSet<Space> Spaces { get; }
        DbSet<Territory> Territories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Update(object entity);
        EntityEntry Add(object entity);
    }
}