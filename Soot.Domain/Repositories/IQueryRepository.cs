using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Soot.Domain.Contact;

namespace Soot.Domain.Repositories
{
    public interface IContactQueryRepository
    {
        public Task<List<ContactDto>> GetAllAsync();
        public Task<List<ContactDto>> GetAllAsync(string sourceName);
    }
}
