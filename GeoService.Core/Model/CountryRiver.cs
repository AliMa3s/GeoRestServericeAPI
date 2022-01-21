using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Core.Model {
    public class CountryRiver {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int RiverId { get; set; }

        public virtual Country Country { get; set; }
        public virtual River River { get; set; }
    }
}
