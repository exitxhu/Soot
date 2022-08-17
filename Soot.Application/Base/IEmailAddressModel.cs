using System.Text.Json.Serialization;
using Soot.Domain.ValueObjects;

namespace Soot.Application.Base;

public interface IEmailAddressModel
{
    public string MailAddress { get; set; }
    [JsonIgnore]
    public EmailAddress Email { get; }

}