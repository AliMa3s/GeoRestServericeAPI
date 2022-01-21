using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Core.Model {
    public class Continent {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        private List<Country> _countries { get; set; }
        public ICollection<Country> countries => _countries;

        public Continent() {
            _countries = new List<Country>();
        }

        public Continent(string name, int population) {
            Name = name;
            Population = population;
            _countries = new List<Country>();
        }
    }
}
