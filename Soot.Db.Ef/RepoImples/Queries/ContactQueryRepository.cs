using Microsoft.EntityFrameworkCore;
using Soot.Domain.Repositories;
using Soot.Domain.Entities;

namespace Soot.Db.Ef.RepoImples.Queries
{
    public class ContactQueryRepository : IContactQueryRepository
    {
        public SootContext Context { get; }
        private readonly IQueryable<Contact?> _contactsQuery;
        public ContactQueryRepository(SootContext context)
        {
            this.Context = context;
            _contactsQuery = context.Contacts.AsNoTracking();
        }
        public async Task<List<Contact.ContactDto>> GetAllAsync()
        {
            return await _contactsQuery.Select(n => n.ToContactDto()).ToListAsync();
        }
        public async Task<List<Contact.ContactDto>> GetAllAsync(string sourceName)
        {
            return await _contactsQuery.Where(n => n.ExternalMappings.Any(m => m.ExternalSourceName != null && m.ExternalSourceName.Equals(sourceName))).Select(n => n.ToContactDto()).ToListAsync();
        }
    }
}
