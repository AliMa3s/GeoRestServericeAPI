using GeoService.Core.Model;
using GeoService.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Core.Service {
   public interface IContinentService :IService<Continent> {
        Task<Continent> GetWithC(int continentId);
        Task<Continent> GetWithCC(int continentId, int countryId);
    }
}
