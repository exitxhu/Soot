using static Soot.Domain.Entities.Contact;

namespace Soot.Domain.Repositories
{
    public interface IContactQueryRepository
    {
        public Task<List<ContactDto>> GetAllAsync();
        public Task<List<ContactDto>> GetAllAsync(string sourceName);
    }
}
