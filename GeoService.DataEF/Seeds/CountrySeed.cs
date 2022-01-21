using GeoService.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.DataEF.Seeds {
    public class CountrySeed : IEntityTypeConfiguration<Country> {

        public void Configure(EntityTypeBuilder<Country> builder) {
            builder.HasData(
                new Country { Id =1, ContinentId = 1, Name = "Belgium", Population = 8000},
                new Country { Id =2, ContinentId = 1, Name = "Germany", Population = 8000},
                new Country { Id =3, ContinentId = 2, Name = "China", Population = 12000},
                new Country { Id =4, ContinentId = 2, Name = "Japan", Population = 12000},
                new Country { Id =5, ContinentId = 2, Name = "Turkey", Population = 15000},
                new Country { Id =6, ContinentId = 2, Name = "Iran", Population = 15000},
                new Country { Id =7, ContinentId = 3, Name = "Nigeria", Population = 15000},
                new Country { Id =8, ContinentId = 4, Name = "Usa", Population = 15000},
                new Country { Id =9, ContinentId = 2, Name = "Russia", Population = 20000},
                new Country { Id =10, ContinentId = 1, Name = "England", Population = 20000}
                );
        }
    }
}
