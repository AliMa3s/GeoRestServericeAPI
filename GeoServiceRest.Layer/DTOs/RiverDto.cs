using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.DTOs {
    public class RiverDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        [Required(ErrorMessage = "{0} moet ingevuld worden")]
        public List<CountryRiverDto> countryRiver { get; set; }
    }
}
