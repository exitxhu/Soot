using Microsoft.AspNetCore.Mvc;
using Soot.Application.Command;
using Soot.Domain.Repositories;
using static Soot.Domain.Entities.Contact;

namespace Soot.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        private readonly ILogger<ManageController> _logger;
        private readonly IUpsertContacts _upsertContacts;
        private readonly IContactQueryRepository _contactQueryRepository;
        public ManageController(ILogger<ManageController> logger,
            IUpsertContacts upsertContacts, IContactQueryRepository contactQueryRepository)
        {
            this._logger = logger;
            this._upsertContacts = upsertContacts;
            this._contactQueryRepository = contactQueryRepository;
        }
        [HttpPost("[action]")]
        public async Task UpsertContacts(List<UpsertContactModel> model)
        {
            _upsertContacts.Model = model;
            await _upsertContacts.ExecuteAsync();
        }
        [HttpDelete("[action]")]
        public async Task DeleteContacts(List<DeleteContactModel> model, [FromServices] IDeleteContacts deleteContacts)
        {
            deleteContacts.Model = model;
            await deleteContacts.ExecuteAsync();
        }
        [HttpGet("[action]")]
        public async Task<List<ContactDto>> GetContacts()
        {
           return await _contactQueryRepository.GetAllAsync();
        }
        [HttpGet("[action]/{sourceName}")]
        public async Task<List<ContactDto>> GetContacts(string sourceName)
        {
            return await _contactQueryRepository.GetAllAsync(sourceName);
        }
    }

}
