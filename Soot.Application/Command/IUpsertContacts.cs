using Soot.Application.Base;
using Soot.Application.Services;
using Soot.Domain;
using Soot.Domain.Repositories;
using Soot.Domain.Shared;
using System.Text.Json.Serialization;
using linqPlusPlus;
using Soot.Application.Exceptions;
using Soot.Domain.Entities;
using Soot.Domain.ValueObjects;
using static Soot.Domain.Entities.Contact;

namespace Soot.Application.Command
{
    public interface IUpsertContacts
    {
        Task<ResultDto> ExecuteAsync();
        public List<UpsertContactModel> Model { get; set; }

    }
    public class UpsertContacts : IUpsertContacts
    {
        private readonly IContactRepository contactRepository;

        public UpsertContacts(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public List<UpsertContactModel> Model { get; set; }

        public async Task<ResultDto> ExecuteAsync()
        {
            var res = new ResultDto();
            try
            {
                InvalidModelException.ThrowIfInvalid(Model);
                var sourceName = Model.FirstOrDefault()?.SourceName;
                var check = await contactRepository.GetAllByExternalIdAsync(Model.Select(n => n.ExternalId), Model.FirstOrDefault()?.SourceName);
                var news = Model
                    .Where(n => !check.Any(a => a.ExternalMappings.Any(n => n.ExternalId == n.ExternalId && n.ExternalSourceName == sourceName)))
                    .Select(n => new Contact
                    {
                        Name = n.Name,
                        EmailAddress = n.Email,
                        ExternalMappings = new List<Contact.ExternalContactMapping>
                         {new Contact.ExternalContactMapping{ExternalId = n.ExternalId,ExternalSourceName = sourceName,}},
                        MobileNumber = n.Mobile,
                        WebSocket = n.WebSocket,
                        Tags = n.Tags.Select(n => new ContactTag { Tag = n }).ToList()
                    }).ToList();
                check.ForEach(n =>
                {
                    var temp = Model
                        .FirstOrDefault(m => n.ExternalMappings.Any(x => m.ExternalId == x.ExternalId && x.ExternalSourceName.Equals(m.SourceName, StringComparison.InvariantCultureIgnoreCase)));
                    n.Merge(temp);
                });
                contactRepository.AddRange(news);
                contactRepository.UpdateRange(check!);
                await contactRepository.SaveAsync();
                res.IsSuccessful = true;
                return res;
            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;
                res.Data = ex;
                res.Message = ex.Message;
                return res;
            }
        }
    }
    public class UpsertContactModel : ModelBase, IContactIdModel, IEmailAddressModel, IMobileNumberModel, IExternalIdModel, IWebsocketModel, ITagModel
    {
        public override bool IsValid =>
            SourceName.HasContent() &&
            ExternalId.HasContent() &&
            !(!MailAddress.HasContent() && !ContactId.HasValue && !MobileNumber.HasContent() && WebSocket is null);

        public int? ContactId { get; set; }
        public string? Name { get; set; }
        public string? ExternalId { get; set; }
        public string? SourceName { get; set; }
        public string? MobileNumber { get; set; }
        public Uri? WebSocket { get; set; }
        public List<string> Tags { get; set; }
        public string? MailAddress { get; set; }
        [JsonIgnore]
        public EmailAddress? Email => MailAddress.HasContent() ? new EmailAddress(MailAddress) : null;
        [JsonIgnore]
        public MobileNumber? Mobile => MobileNumber.HasContent() ? new MobileNumber(MobileNumber) : null;
    }

}
