using Soot.Domain.Base;
using Soot.Domain.Repositories;

namespace Soot.Db.Ef.RepoImples
{
    public abstract class Repository<T> : IRepository<T> where T : Root<T>, new()
    {
        protected readonly SootContext DbContext;

        protected Repository(SootContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public void Add(T entity)
        {
            DbContext.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            DbContext.AddRange(entities);
        }

        public void Delete(T entity)
        {
            DbContext.Remove(entity);
        }

        public void Delete(object id)
        {
            var entity = new T().SetTrueId(id);
            DbContext.Attach(entity);
            DbContext.Set<T>().Remove(entity);

        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            DbContext.RemoveRange(entities);
        }

        public void DeleteRange(IEnumerable<object> ids)
        {
            var entity = ids.Select(n=> new T().SetTrueId(n));
            DbContext.AttachRange(entity);
            DbContext.Set<T>().RemoveRange(entity);
        }

        public abstract Task<T?> GetByIdAsync(object? id);

        public async Task<int> SaveAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            DbContext.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            DbContext.UpdateRange(entities);
        }
    }
}
