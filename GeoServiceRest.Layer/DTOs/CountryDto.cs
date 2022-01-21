using GeoServiceRest.Layer.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.DTOs {
    [ServiceFilter(typeof(NotFoundFilterCountry))]
    public class CountryDto {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} Moet ingevuld worden")]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "{0} Moet groter zijn dan 0")]
        public int Population { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "{0} Moet groter zijn dan 0")]
        public double Surface { get; set; }
        public ContinentDto Continent { get; set; }
        public int ContinentId { get; set; }
        public List<CountryRiverDto> countryRiver { get; set; }
    }
}
