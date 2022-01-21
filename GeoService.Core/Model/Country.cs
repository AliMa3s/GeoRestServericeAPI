using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Core.Model {
    public class Country {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public double Surface { get; set; }
        public virtual Continent Continent { get; set; }
        public int ContinentId { get; set; }
        private List<City> _cities { get; set; }
        public ICollection<City> cities => _cities;
        private List<CountryRiver> _countryRiver { get; set; }
        public ICollection<CountryRiver> countryRiver => _countryRiver;

        public Country() {
            _cities = new List<City>();
            _countryRiver = new List<CountryRiver>();
        }

        public Country(Continent continent) {
            Continent = continent;
            _cities = new List<City>();
            _countryRiver = new List<CountryRiver>();
        }
    }
}
