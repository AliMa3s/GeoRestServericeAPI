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

    public class CountryRiverConfiguration : IEntityTypeConfiguration<CountryRiver> {


        public void Configure(EntityTypeBuilder<CountryRiver> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CountryId).HasColumnName("Country_Id");
            builder.Property(x => x.RiverId).HasColumnName("River_Id");
            builder.HasOne(x => x.Country)
                .WithMany(x => x.countryRiver)
                .HasForeignKey(x => x.CountryId)
                .HasConstraintName("FK_Country_River_Country");
            builder.HasOne(x => x.River)
                .WithMany(x => x.countryRiver)
                .HasForeignKey(x => x.RiverId)
                .HasConstraintName("FK_Country_River_River");
            builder.ToTable("Country_River");
        }
    }
}
