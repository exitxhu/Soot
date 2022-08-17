using Soot.Domain.Entities;
using Soot.Domain.ValueObjects;

namespace Soot.Domain.Repositories;

public interface IContactRepository : IRepository<Contact>
{
    Task<List<Contact?>> GetAllByExternalIdAsync(IEnumerable<string?> externalId, string sourceName);
    Task<List<Contact?>> GetAllByEmailAddressAsync(IEnumerable<EmailAddress> email);
    Task<List<Contact?>> GetAllByMobileNumberAsync(IEnumerable<MobileNumber> mobile);
    Task<Contact?> GetByExternalIdAsync(string? externalId, string sourceName);
    Task<Contact?> GetByEmailAddressAsync(EmailAddress email);
    Task<Contact?> GetByMobileNumberAsync(MobileNumber mobile);
    Task<List<Contact?>> GetAllByIdAsync(IEnumerable<int> ids);
}