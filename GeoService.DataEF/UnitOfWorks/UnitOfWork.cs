using GeoService.Core.Repository;
using GeoService.Core.UnitOfWorks;
using GeoService.DataEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.DataEF.UnitOfWorks {
    public class UnitOfWork : IUnitOfWorks {
        private AppDBContext _context;
        private ContinentRepository _continentRepository;
        public UnitOfWork(AppDBContext appDbContext) {
            _context = appDbContext;
        }

        public IContinentRepository continents => _continentRepository ?? new ContinentRepository(_context);

        public void Commit() {
            _context.SaveChanges();
        }

        public async Task CommitAsync() {
            await _context.SaveChangesAsync();
        }
    }
}
