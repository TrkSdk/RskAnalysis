﻿
using RskAnalysis.CORE.IntServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntUnitOfWork;

namespace RskAnalysis.SERVICE
{
    public class Service<T> : IService<T> where T : class
    {
        public readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<T> _repo;

        public Service(IUnitOfWork unitOfWork, IRepository<T> repo)
        {
            _UnitOfWork = unitOfWork;
            _repo = repo;
        }

        public async Task<T?> AddAsync(T? entity)
        {
           
            await _repo.AddAsync(entity);
            await _UnitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T?>> AddRangeAsync(IEnumerable<T?> entities)
        {
            await _repo.AddRangeAsync(entities);
            await _UnitOfWork.CommitAsync();
            return entities;
        }

        public async Task<IEnumerable<T?>> Where(Expression<Func<T?, bool>> predicate)
        {
            return await _repo.Where(predicate);
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        
        public async Task<T> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public void Remove(T? entity)
        {
            _repo.Remove(entity);
            _UnitOfWork.Commit();
        }

        public void RemoveRange(IEnumerable<T?> entities)
        {
            _repo.RemoveRange(entities);
            _UnitOfWork.Commit();
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T?, bool>> predicate)
        {
            return await _repo.SingleOrDefaultAsync(predicate);
        }

        public T? Update(T? entity)
        {
            T? upd = _repo.Update(entity);
            _UnitOfWork.Commit();
            return upd;
        }

        
    }
}
