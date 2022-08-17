using System.Text.Json.Serialization;
using linqPlusPlus;
using Soot.Application.Base;
using Soot.Domain.ValueObjects;

namespace Soot.Application.Command;

public class UpsertContactModel : ModelBase, IContactIdModel, IEmailAddressModel, IMobileNumberModel, IExternalIdModel, IWebsocketModel, ITagModel
{
    public UpsertContactModel(int? contactId, string? name, string? externalId, string? sourceName, string mobileNumber, Uri webSocket, List<string> tags, string mailAddress)
    {
        ContactId = contactId;
        Name = name;
        ExternalId = externalId;
        SourceName = sourceName;
        MobileNumber = mobileNumber;
        WebSocket = webSocket;
        Tags = tags;
        MailAddress = mailAddress;
    }

    public override bool IsValid =>
        SourceName.HasContent() &&
        ExternalId.HasContent() &&
        !(!MailAddress.HasContent() && !ContactId.HasValue && !MobileNumber.HasContent() && WebSocket is null);

    public int? ContactId { get; set; }
    public string? Name { get; set; }
    public string? ExternalId { get; set; }
    public string? SourceName { get; set; }
    public string MobileNumber { get; set; }
    public Uri WebSocket { get; set; }
    public List<string> Tags { get; set; }
    public string MailAddress { get; set; }
    [JsonIgnore]
    public EmailAddress Email => MailAddress.HasContent() ? new EmailAddress(MailAddress) : null;
    [JsonIgnore]
    public MobileNumber Mobile => MobileNumber.HasContent() ? new MobileNumber(MobileNumber) : null;
}