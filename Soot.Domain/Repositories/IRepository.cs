using Soot.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soot.Domain.Entities;
using Soot.Domain.ValueObjects;

namespace Soot.Domain.Repositories
{
    public interface IRepository<T> where T : Root<T>, new()
    {
        Task<T?> GetByIdAsync(object id);
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
    public interface IContactRepository : IRepository<Contact>
    {
        Task<List<Contact?>> GetAllByExternalIdAsync(IEnumerable<string> externalId, string sourceName);
        Task<List<Contact?>> GetAllByEmailAddressAsync(IEnumerable<EmailAddress> email);
        Task<List<Contact?>> GetAllByMobileNumberAsync(IEnumerable<MobileNumber> mobile);
        Task<Contact?> GetByExternalIdAsync(string externalId, string sourceName);
        Task<Contact?> GetByEmailAddressAsync(EmailAddress email);
        Task<Contact?> GetByMobileNumberAsync(MobileNumber mobile);
        Task<List<Contact?>> GetAllByIdAsync(IEnumerable<int> ids);
    }
    public interface IInboxRepository : IRepository<Inbox>
    {

    }
    public interface INotificationRepository : IRepository<Notification>
    {

    }
    public interface ITagRepository : IRepository<Tag>
    {

    }
}
