﻿using GeoService.Core.Repository;
using GeoService.Core.Service;
using GeoService.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Service.Services {
    public class Service<TEntity> : IService<TEntity> where TEntity : class {

        public readonly IUnitOfWorks _unitOfWork;
        private readonly IRepository<TEntity> _repository;

        public Service(IUnitOfWorks unitOfWork, IRepository<TEntity> repository) {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<TEntity> AddAsync(TEntity entity) {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() {
            return await _repository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id) {
            return await _repository.GetByIdAsync(id);
        }

        public void Remove(TEntity entity) {
            _repository.Remove(entity);
            _unitOfWork.Commit();
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate) {
            return await _repository.SingleOrDefault(predicate);
        }

        public TEntity Update(TEntity entity) {
            TEntity updateEntity = _repository.Update(entity);
            _unitOfWork.Commit();
            return updateEntity;
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate) {
            return await _repository.Where(predicate);
        }
    }
}
