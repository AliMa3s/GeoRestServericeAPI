using GeoService.Core.Model;
using GeoService.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.DataEF.Repositories {
    public class ContinentRepository : Repository<Continent>, IContinentRepository {
        private AppDBContext _appDbContext { get => _context as AppDBContext; }
        
        public ContinentRepository(AppDBContext context) : base(context) {

        }

        public async Task<Continent> GetWithC(int continentId) {
            return await _appDbContext.continents.Include(x => x.countries).ThenInclude(x => x.countryRiver)
                .SingleOrDefaultAsync(x => x.Id == continentId);
        }

        public async Task<Continent> GetWithCC(int continentId, int countryId) {
            return await _appDbContext.continents.Include(x => x.countries.Where(x => x.Id == countryId))
                .ThenInclude(x => x.cities).Include(x => x.countries).ThenInclude(x => x.countryRiver)
                .SingleOrDefaultAsync(x => x.Id == continentId);
        }
    }
}
