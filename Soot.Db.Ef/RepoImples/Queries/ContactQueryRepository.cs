using Microsoft.EntityFrameworkCore;
using Soot.Domain;
using Soot.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soot.Db.Ef.RepoImples.Queries
{
    public class ContactQueryRepository : IContactQueryRepository
    {
        private readonly SootContext context;
        private IQueryable<Contact> contactsQuery;
        public ContactQueryRepository(SootContext context)
        {
            this.context = context;
            contactsQuery = context.Contacts.AsNoTracking();
        }
        public async Task<List<Contact.ContactDto>> GetAllAsync()
        {
            return await contactsQuery.Select(n => n.ToContactDto()).ToListAsync();
        }

        public async Task<List<Contact.ContactDto>> GetAllAsync(string sourceName)
        {
            return await contactsQuery.Where(n => n.ExternalMappings.Any(m => m.ExternalSourceName.Equals(sourceName))).Select(n => n.ToContactDto()).ToListAsync();
        }
    }
}
