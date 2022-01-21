using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoService.Core.Model;
using GeoService.DataEF.Configuration;
using GeoService.DataEF.Seeds;

namespace GeoService.DataEF {
   public class AppDBContext : DbContext{

        public AppDBContext(DbContextOptions options) : base(options) {

        }

        public DbSet<Continent> continents { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<City> cities{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new ContinentConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new RiverConfiguration());
            modelBuilder.ApplyConfiguration(new CountryRiverConfiguration());
            modelBuilder.ApplyConfiguration(new CitySeed());
            modelBuilder.ApplyConfiguration(new CountrySeed());
            modelBuilder.ApplyConfiguration(new ContinentSeed());
            modelBuilder.ApplyConfiguration(new RiverSeed());
            modelBuilder.ApplyConfiguration(new CountryRiverSeed());
        }
    }
}
