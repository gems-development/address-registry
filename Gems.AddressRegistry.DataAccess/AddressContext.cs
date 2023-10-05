using Gems.AddressRegistry.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gems.AddressRegistry.DataAccess
{
    public class AddressContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        private readonly string _connectionString = "Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=admin";

        public AddressContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}