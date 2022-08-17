using Soot.Domain.Base;

namespace Soot.Domain.Repositories
{
    public interface IRepository<T> where T : Root<T>, new()
    {
        Task<T?> GetByIdAsync(object? id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(object id);
        void DeleteRange(IEnumerable<object> ids);
        void DeleteRange(IEnumerable<T> entities);
        Task<int> SaveAsync();
    }
}
