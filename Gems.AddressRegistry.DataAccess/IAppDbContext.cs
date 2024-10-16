﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.DataAccess
{
    public interface IAppDbContext
    {
        DbSet<Address> Addresses { get; }
        DbSet<InvalidAddress> InvalidAddresses { get; }
        DbSet<Region> Regions { get; }
        DbSet<AdministrativeArea> AdministrativeAreas { get; }
        DbSet<MunicipalArea> MunicipalAreas { get; }
        DbSet<Territory> Territories { get; }
        DbSet<City> Cities { get; }
        DbSet<Settlement> Settlements { get; }
        DbSet<PlaningStructureElement> PlaningStructureElements { get; }
        DbSet<RoadNetworkElement> RoadNetworkElements { get; }
        DbSet<Building> Buildings { get; }
        DbSet<DataSourceBase> DataSource { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Update(object entity);
        EntityEntry Add(object entity);
    }
}