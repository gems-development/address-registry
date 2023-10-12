using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Gems.AddressRegistry.DataAccess
{
    public class AddressContext : DbContext
    {
        public DbSet<Address> Addresses { get;}

        private readonly string _connectionString = "Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=admin";

        public AddressContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataSourceBase>().HasDiscriminator();
            modelBuilder.Entity<DataSourceBase>().ToTable("DataSource");

        }
    }
}