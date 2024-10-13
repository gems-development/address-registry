using Microsoft.EntityFrameworkCore;
using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.Common;

namespace Gems.AddressRegistry.DataAccess
{
	public class AppDbContext : DbContext, IAppDbContext
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
		public DbSet<DataSourceBase> DataSource {  get; set; }

		private readonly string _connectionString = "Host=localhost;Port=5442;Database=addressdb;Username=postgres;Password=admin";
		private readonly bool _asNoTracking;

		public AppDbContext()
		{ }

		public AppDbContext(string connectionString, bool asNoTracking)
		{
			_connectionString = connectionString;
			_asNoTracking = asNoTracking;
		}

		public AppDbContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.UseLazyLoadingProxies()
				.UseNpgsql(_connectionString);

			if (_asNoTracking)
				optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Country>().HasNoKey();
			modelBuilder.Entity<DataSourceBase>().HasDiscriminator();
			modelBuilder.Entity<DataSourceBase>().ToTable("DataSource");
			modelBuilder.Entity<DataSourceBase>().HasKey(nameof(DataSourceBase.Id), nameof(DataSourceBase.SourceType));
			modelBuilder.Entity<DataSourceBase>().Property(o => o.SourceType).HasConversion<string>();
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
