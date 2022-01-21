using GeoService.Core.Model;
using GeoService.Core.Repository;
using GeoService.Core.Service;
using GeoService.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Service.Services {
    public class ContinentService : Service<Continent>, IContinentService {

        public ContinentService(IUnitOfWorks unitOfWorks, IRepository<Continent> repository) : base(unitOfWorks, repository) {

        }

        public async Task<Continent> GetWithC(int continentId) {
            return await _unitOfWork.continents.GetWithC(continentId);
        }

        public async Task<Continent> GetWithCC(int continentId, int countryId) {
            return await _unitOfWork.continents.GetWithCC(continentId, countryId);
        }

      
    }
}
