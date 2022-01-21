using GeoService.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.DataEF.Seeds {
    public class CountryRiverSeed : IEntityTypeConfiguration<CountryRiver> {

        public void Configure(EntityTypeBuilder<CountryRiver> builder) {
            builder.HasData(
                new CountryRiver { Id = 1, CountryId = 2, RiverId = 1},
                new CountryRiver { Id = 2, CountryId = 3, RiverId = 2},
                new CountryRiver { Id = 3, CountryId = 4, RiverId = 4},
                new CountryRiver { Id = 4, CountryId = 1, RiverId = 3},
                new CountryRiver { Id = 5, CountryId = 5, RiverId = 5},
                new CountryRiver { Id = 6, CountryId = 10, RiverId = 6},
                new CountryRiver { Id = 7, CountryId = 9, RiverId = 7},
                new CountryRiver { Id = 8, CountryId = 7, RiverId = 10},
                new CountryRiver { Id = 9, CountryId = 6, RiverId = 9},
                new CountryRiver { Id = 10, CountryId = 8, RiverId = 8}
                );
        }
    }
}
