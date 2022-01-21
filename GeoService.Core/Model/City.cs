using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Core.Model {
   public class City {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }

        public City() {
        }

        public City(Country country) {
            Country = country;
        }
    }
}
