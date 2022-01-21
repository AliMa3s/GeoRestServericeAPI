using GeoService.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.DataEF.Seeds {
    public class RiverSeed : IEntityTypeConfiguration<River> {

        public void Configure(EntityTypeBuilder<River> builder) {
            builder.HasData(
                new River { Id = 1, Name = "Berlins Wall River", Length = 5464},
                new River { Id = 2, Name = "Salween River", Length = 2348}, //China
                new River { Id = 3, Name = "Gent Kanal", Length = 6300}, //belgium
                new River { Id = 4, Name = "japanees blosom river", Length = 1012},
                new River { Id = 5, Name = "Istanbul River", Length = 4200},
                new River { Id = 6, Name = "London bridge river", Length = 3625},
                new River { Id = 7, Name = "Moscow kanal", Length = 3142},
                new River { Id = 8, Name = "New York River", Length = 890},
                new River { Id = 9, Name = "Tehran River", Length = 4200},
                new River { Id = 10, Name = "Abotalal River", Length = 3000}
                );
        }
    }
}
