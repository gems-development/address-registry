using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Gems.AddressRegistry.DataAccess
{
    internal class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<InvalidAddress> InvalidAddresses { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<AdministrativeArea> AdministrativeAreas { get; set; }
        public DbSet<MunicipalArea> MunicipalAreas { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<PlaningStructureElement> PlaningStructureElements { get; set; }
        public DbSet<RoadNetworkElement> RoadNetworkElements { get; set; }
        public DbSet<Building> Buildings { get; set; }

        private readonly string _connectionString = "Host=localhost;Port=5432;Database=addressdb;Username=postgres;Password=postgres";

        public AppDbContext()
        { }

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseNpgsql(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasNoKey();
            modelBuilder.Entity<DataSourceBase>().HasDiscriminator();
            modelBuilder.Entity<DataSourceBase>().ToTable("DataSource");
            modelBuilder.Entity<DataSourceBase>().HasKey(nameof(DataSourceBase.Id), nameof(DataSourceBase.SourceType));

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            FillAuditableFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void FillAuditableFields()
        {
            var auditableEntities = ChangeTracker
              .Entries<BaseAuditableEntity>()
              .Select(o => o.Entity)
              .ToArray();

            foreach (var entity in auditableEntities)
                entity.Updated = DateTime.UtcNow;

            var addedAuditableEntities = ChangeTracker
              .Entries<BaseAuditableEntity>()
              .Where(o => o.State == EntityState.Added)
              .Select(o => o.Entity)
              .ToArray();

            foreach (var entity in addedAuditableEntities)
                entity.Created = DateTime.UtcNow;
        }

    }
}