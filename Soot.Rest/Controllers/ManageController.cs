using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soot.Application.Command;
using Soot.Domain;
using Soot.Domain.Repositories;
using System.Text.Json.Serialization;
using static Soot.Domain.Contact;

namespace Soot.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        private readonly ILogger<ManageController> logger;
        private readonly IUpsertContacts upsertContacts;
        private readonly IContactQueryRepository contactQueryRepository;

        public ManageController(ILogger<ManageController> logger,
            IUpsertContacts upsertContacts, IContactQueryRepository contactQueryRepository)
        {
            this.logger = logger;
            this.upsertContacts = upsertContacts;
            this.contactQueryRepository = contactQueryRepository;
        }
        [HttpPost("[action]")]
        public async Task UpsertContacts(List<UpsertContactModel> model)
        {
            upsertContacts.Model = model;
            await upsertContacts.ExecuteAsync();
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
           return await contactQueryRepository.GetAllAsync();
        }
        [HttpGet("[action]/{sourceName}")]
        public async Task<List<ContactDto>> GetContacts(string sourceName)
        {
            return await contactQueryRepository.GetAllAsync(sourceName);
        }
    }

}
