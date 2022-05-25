using Microsoft.EntityFrameworkCore;
using Soot.Domain.Base;
using Soot.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soot.Db.Ef.RepoImples
{
    public abstract class Repository<T> : IRepository<T> where T : Root<T>, new()
    {
        protected readonly SootContext _dbContext;

        public Repository(SootContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void Add(T entity)
        {
            _dbContext.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.AddRange(entities);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void Delete(object id)
        {
            var entity = new T().SetTrueId(id);
            _dbContext.Attach(entity);
            _dbContext.Set<T>().Remove(entity);

        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public void DeleteRange(IEnumerable<object> ids)
        {
            var entity = ids.Select(n=> new T().SetTrueId(n));
            _dbContext.AttachRange(entity);
            _dbContext.Set<T>().RemoveRange(entity);
        }

        public abstract Task<T?> GetByIdAsync(object id);

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.UpdateRange(entities);
        }
    }
}
