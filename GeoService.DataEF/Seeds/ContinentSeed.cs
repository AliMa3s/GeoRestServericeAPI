using GeoService.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.DataEF.Seeds {
    public class ContinentSeed : IEntityTypeConfiguration<Continent> {

        public void Configure(EntityTypeBuilder<Continent> builder) {
            builder.HasData(
                new Continent { Id = 1, Name = "Europe", Population = 8000 },
                new Continent { Id = 2, Name = "Asia", Population = 8000 },
                new Continent { Id = 3, Name = "Afrika", Population = 12000 },
                new Continent { Id = 4, Name = "North-America", Population = 12000 },
                new Continent { Id = 5, Name = "South-America", Population = 15000 }

    );
        }
    }
}
