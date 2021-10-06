using System;
using FlowerHub.Infrustructure;
using FlowersHub.Model;
using Microsoft.EntityFrameworkCore;

namespace FlowersHub.Data
{
    public class FlowersHubContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<ColorType> Colors { get; set; }
        public DbSet<FlowerType> FlowerTypes { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public DbSet<Source> Sources { get; set; }
        
        public FlowersHubContext()
        {
            _connectionString = SqlConfigurationHelper.ConnectionString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
