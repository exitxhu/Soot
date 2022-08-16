using Soot.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Soot.Domain.ValueObjects;

namespace Soot.Application.Base
{
    public abstract class ModelBase
    {
        public abstract bool IsValid { get; }
    }
    public interface IEmailAddressModel
    {
        public string? MailAddress { get; set; }
        [JsonIgnore]
        public EmailAddress? Email { get; }

    }
    public interface IExternalIdModel
    {
        public string? ExternalId { get; set; }
        public string? SourceName { get; set; }
    }
    public interface IMobileNumberModel
    {
        public string? MobileNumber { get; set; }
        [JsonIgnore]
        public MobileNumber? Mobile { get; }
    }
    public interface IContactIdModel
    {
        public int? ContactId { get; set; }
    }
    public interface IWebsocketModel
    {
        public Uri? WebSocket { get; set; }
    }
    public interface ITagModel
    {
        public List<string> Tags { get; set; }
    }
}
