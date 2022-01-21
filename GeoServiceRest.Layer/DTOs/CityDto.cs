using GeoServiceRest.Layer.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceRest.Layer.DTOs {
    [ServiceFilter(typeof(NotFoundFilterCity))]
    public class CityDto {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} Moet ingevuld worden")]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "{0} Moet groter zijn dan 0")]
        public int Population { get; set; }
        public CountryDto Country { get; set; }
        public int CountryId { get; set; }
    }
}
