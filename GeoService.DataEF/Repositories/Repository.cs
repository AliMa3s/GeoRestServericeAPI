using GeoService.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.DataEF.Repositories {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {

        protected readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDBContext context) {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id) {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(TEntity entity) {
            _dbSet.Remove(entity);
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate) {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity) {
            _context.ChangeTracker.Clear();
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate) {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
