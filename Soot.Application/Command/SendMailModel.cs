using System.Text.Json.Serialization;
using linqPlusPlus;
using Soot.Application.Base;
using Soot.Domain.ValueObjects;

namespace Soot.Application.Command;

public class SendMailModel : ModelBase, IEmailAddressModel, IExternalIdModel, IContactIdModel
{
    public SendMailModel()
    {
        
    }
    public SendMailModel(int? contactId, string subject, string body, string mailAddress, string? externalId, string? sourceName)
    {
        ContactId = contactId;
        Subject = subject;
        Body = body;
        MailAddress = mailAddress;
        ExternalId = externalId;
        SourceName = sourceName;
    }
    public int? ContactId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public override bool IsValid =>
        !(string.IsNullOrEmpty(MailAddress) && string.IsNullOrEmpty(ExternalId) && !ContactId.HasValue);
    public string MailAddress { get; set; }
    public string? ExternalId { get; set; }
    public string? SourceName { get; set; }
    [JsonIgnore]
    public EmailAddress Email => MailAddress.HasContent() ? new EmailAddress(MailAddress) : null;
}