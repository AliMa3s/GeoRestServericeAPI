using GeoService.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.DataEF.Configuration {
    //IEntityTypeConfiguration<TEntity> Interface
    //Allows configuration for an entity type to be factored into a separate class, rather than in-line in OnModelCreating(ModelBuilder).
    public class CountryConfiguration : IEntityTypeConfiguration<Country> {


        public void Configure(EntityTypeBuilder<Country> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.Population);
            builder.Property(x => x.Surface);
            builder.HasOne(d => d.Continent).WithMany(x => x.countries);
            builder.ToTable("Country");
        }
    }
}
