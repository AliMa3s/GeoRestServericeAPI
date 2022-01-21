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
    public class CityConfiguration : IEntityTypeConfiguration<City> {

        public void Configure(EntityTypeBuilder<City> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.Population);
            builder.HasOne(d => d.Country).WithMany(x => x.cities);
            builder.ToTable("City");
        }
    }
}
