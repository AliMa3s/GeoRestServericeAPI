using GeoService.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Core.Repository {
   public interface IContinentRepository : IRepository<Continent>{
        Task<Continent> GetWithC(int continentId);
        Task<Continent> GetWithCC(int continentId, int countryId);
    }
}
