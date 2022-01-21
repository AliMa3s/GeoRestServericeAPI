using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.DTOs {
    public class CountryRiverDto {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int RiverId { get; set; }
    }
}
