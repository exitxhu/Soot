using System.Text.Json.Serialization;
using linqPlusPlus;
using Soot.Application.Base;
using Soot.Domain.ValueObjects;

namespace Soot.Application.Command;

public class SendSmsModel : ModelBase, IExternalIdModel, IMobileNumberModel, IContactIdModel
{
    public SendSmsModel()
    {
        
    }
    public SendSmsModel(string? externalId, int? contactId, string body, string? sourceName, string mobileNumber)
    {
        ExternalId = externalId;
        ContactId = contactId;
        Body = body;
        SourceName = sourceName;
        MobileNumber = mobileNumber;
    }

    public string? ExternalId { get; set; }
    public int? ContactId { get; set; }
    public string Body { get; set; }
    public string? SourceName { get; set; }
    public override bool IsValid =>
        !(string.IsNullOrEmpty(MobileNumber) && string.IsNullOrEmpty(ExternalId) && !ContactId.HasValue);
    public string MobileNumber { get; set; }
    [JsonIgnore]
    public MobileNumber Mobile => MobileNumber.HasContent() ? new MobileNumber(MobileNumber) : null;
}