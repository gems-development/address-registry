using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Gems.AddressRegistry.DataAccess
{
    internal class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<AdministrativeArea> AdministrativeAreas { get; set; }
        public DbSet<MunicipalArea> MunicipalAreas { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<PlaningStructureElement> PlaningStructureElements { get; set; }
        public DbSet<RoadNetworkElement> RoadNetworkElements { get; set; }
        public DbSet<LandPlot> LandPlots { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Space> Spaces { get; set; }

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
            modelBuilder.Entity<DataSourceBase>().HasDiscriminator();
            modelBuilder.Entity<DataSourceBase>().ToTable("DataSource");

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