using linqPlusPlus;
using Soot.Application.Base;
using Soot.Application.Email;
using Soot.Application.Services;
using Soot.Domain;
using Soot.Domain.Repositories;
using Soot.Domain.Shared;
using System.Text.Json.Serialization;

namespace Soot.Application.Command
{
    public interface ISendForceEmail
    {
        Task<ResultDto> ExecuteAsync();
        public SendMailModel Model { get; set; }

    }
    public class SendForceEmail : ISendForceEmail
    {
        private readonly IEmailModule email;
        private readonly IContactRepository contactRepository;

        public SendForceEmail(IEmailModule email, IContactRepository contactRepository)
        {
            this.email = email;
            this.contactRepository = contactRepository;
        }

        public SendMailModel Model { get; set; }

        public async Task<ResultDto> ExecuteAsync()
        {
            var contact = await Model.CheckThenCreateAsync(contactRepository);
            var res = await email.SendEmailAsync(Model.Body, contact);
            res.Data = Model;
            return res;
        }
    }
    public class SendMailModel : ModelBase, IEmailAddressModel, IExternalIdModel, IContactIdModel
    {


        public int? ContactId { get; set; }
        public string Body { get; set; }

        public override bool IsValid =>
            !(string.IsNullOrEmpty(MailAddress) && string.IsNullOrEmpty(ExternalId) && !ContactId.HasValue);

        public string? MailAddress { get; set; }
        public string? ExternalId { get; set; }
        public string? SourceName { get; set; }

        [JsonIgnore]
        public EmailAddress? Email => MailAddress.HasContent() ? new EmailAddress(MailAddress) : null;
    }

}
