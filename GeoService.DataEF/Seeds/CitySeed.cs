using GeoService.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.DataEF.Seeds {
    public class CitySeed : IEntityTypeConfiguration<City> {

        public void Configure(EntityTypeBuilder<City> builder) {
            builder.HasData(
                new City{Id = 1, CountryId = 1, Name = "Brussel", Population = 8000},
                new City{Id = 2, CountryId = 2, Name = "Berlin", Population = 8000},
                new City{Id = 3, CountryId = 3, Name = "Peking", Population = 12000},
                new City{Id = 4, CountryId = 4, Name = "Tokyo", Population = 12000 },
                new City{Id = 5, CountryId = 5, Name = "Istanbul", Population = 15000},
                new City{Id = 6, CountryId = 6, Name = "Tehran", Population = 15000},
                new City{Id = 7, CountryId = 7, Name = "Nairobi", Population = 15000},
                new City{Id = 8, CountryId = 8, Name = "Newyork", Population = 15000},
                new City{Id = 9, CountryId = 9, Name = "Moscow", Population = 20000},
                new City{Id = 10, CountryId = 10, Name = "London", Population = 20000}
                );
        }
    }
}
