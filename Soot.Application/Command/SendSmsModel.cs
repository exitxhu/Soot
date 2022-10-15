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

public class SendLookupSmsModel : ModelBase, IExternalIdModel, IMobileNumberModel, IContactIdModel
{
    public SendLookupSmsModel()
    {

    }
    public SendLookupSmsModel(string? externalId, int? contactId, string templateId, string? sourceName, string mobileNumber, string token, string token2 = null, string token3 = null)
    {
        ExternalId = externalId;
        ContactId = contactId;
        TemplateId = templateId;
        SourceName = sourceName;
        MobileNumber = mobileNumber;
        Token = token;
        Token2 = token2;
        Token3 = token3;
    }

    public string? ExternalId { get; set; }
    public int? ContactId { get; set; }
    public string TemplateId { get; set; }
    public string Token { get; set; }
    public string Token2 { get; set; }
    public string Token3 { get; set; }
    public string? SourceName { get; set; }
    public override bool IsValid =>
        !(string.IsNullOrEmpty(MobileNumber) && string.IsNullOrEmpty(ExternalId) && !ContactId.HasValue);
    public string MobileNumber { get; set; }
    [JsonIgnore]
    public MobileNumber Mobile => MobileNumber.HasContent() ? new MobileNumber(MobileNumber) : null;
}