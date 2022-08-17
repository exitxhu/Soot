using Soot.Domain.Repositories;

namespace Soot.Application.Services
{
    public class ContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }
    }
}
