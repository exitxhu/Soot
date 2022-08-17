using Microsoft.EntityFrameworkCore;
using Soot.Domain.Entities;
using Soot.Domain.Repositories;
using Soot.Domain.ValueObjects;

namespace Soot.Db.Ef.RepoImples
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private IQueryable<Contact?> ContactQuery => DbContext.Contacts.Include(n => n.ExternalMappings).Include(n => n.Tags);
        public ContactRepository(SootContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Contact?>> GetAllByEmailAddressAsync(IEnumerable<EmailAddress> email)
        {
            var res = new List<Contact?>();
            if (email.Count() == 1)
            {
                var address = await GetByEmailAddressAsync(email.First());
                res = address is not null ? res.Append(address).ToList() : res;
            }
            else
                await GetAll(email, res);
            return res;

            async Task GetAll(IEnumerable<EmailAddress> email, List<Contact?> res)
            {
                res.AddRange(await ContactQuery.Where(n => email.Contains(n.EmailAddress)).ToListAsync());
            }
        }
        public async Task<List<Contact?>> GetAllByMobileNumberAsync(IEnumerable<MobileNumber?> mobile)
        {
            var res = new List<Contact>();
            if (mobile.Count() == 1)
            {
                var tres = await GetByMobileNumberAsync(mobile.First());
                res = tres is not null ? res.Append(tres).ToList() : res;
            }
            else
                await GetAll(mobile, res);
            return res;

            async Task GetAll(IEnumerable<MobileNumber> mobile, List<Contact> res)
            {
                res.AddRange(await ContactQuery.Where(n => mobile.Contains(n.MobileNumber)).ToListAsync());
            }
        }
        public async Task<List<Contact?>> GetAllByExternalIdAsync(IEnumerable<string?> externalId, string sourceName)
        {
            var res = new List<Contact?>();
            if (externalId.Count() == 1)
            {
                var tres = await GetByExternalIdAsync(externalId.First(), sourceName);
                res = tres is not null ? res.Append(tres).ToList() : res;
            }
            else
                await GetAll(externalId, sourceName, res);
            return res;

            async Task GetAll(IEnumerable<string?> externalId, string sourceName, List<Contact?> res)
            {
                res.AddRange(await ContactQuery
                .Where(n => n.ExternalMappings.Any(n => externalId.Contains(n.ExternalId) && n.ExternalSourceName == sourceName)).ToListAsync());
            }
        }
        public override async Task<Contact?> GetByIdAsync(object? id)
        {
            return await DbContext.Contacts.FindAsync(id);
        }
        public async Task<List<Contact?>> GetAllByIdAsync(IEnumerable<int> ids)
        {
            var res = new List<Contact>();
            if (ids.Count() == 1)
                res.Add(await GetByIdAsync(ids.First()));
            else
                await GetAll(ids, res);
            return res;

            async Task GetAll(IEnumerable<int> ids, List<Contact> res)
            {
                res.AddRange(await ContactQuery.Where(n => ids.Contains(n.ContactId)).ToListAsync());
            }
        }
        public async Task<Contact?> GetByExternalIdAsync(string? externalId, string sourceName)
        {
            return await ContactQuery.Include(n => n.ExternalMappings)
                    .SingleOrDefaultAsync(n => n.ExternalMappings.Any(n => n.ExternalId == externalId && n.ExternalSourceName == sourceName));
        }
        public async Task<Contact?> GetByEmailAddressAsync(EmailAddress email)
        {
            return await ContactQuery.SingleOrDefaultAsync(n => n.EmailAddress == email);
        }
        public async Task<Contact?> GetByMobileNumberAsync(MobileNumber mobile)
        {
            return await ContactQuery.SingleOrDefaultAsync(n => n.MobileNumber == mobile);
        }
    }
}
