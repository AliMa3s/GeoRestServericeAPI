using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.DTOs {
    public class CountryWithCDto {
        public List<CityDto> Cities { get; set; }
    }
}
