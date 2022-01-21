using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Core.Model {
    public class River {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        private List<CountryRiver> _countryRiver { get; set; }
        public ICollection<CountryRiver> countryRiver => _countryRiver;

        public River() {
            _countryRiver = new List<CountryRiver>();
        }
    }
}
