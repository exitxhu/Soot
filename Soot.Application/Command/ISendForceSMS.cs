using linqPlusPlus;
using Soot.Application.Base;
using Soot.Application.Email;
using Soot.Application.Services;
using Soot.Application.Sms;
using Soot.Domain;
using Soot.Domain.Repositories;
using Soot.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Soot.Application.Command
{
    public interface ISendForceSms
    {
        Task<ResultDto> ExecuteAsync();
        public SendSmsModel Model { get; set; }

    }
    public class SendForceSms : ISendForceSms
    {
        private readonly ISmsModule sms;
        private readonly IContactRepository contactRepository;

        public SendForceSms(ISmsModule sms, IContactRepository contactRepository)
        {
            this.sms = sms;
            this.contactRepository = contactRepository;
        }

        public SendSmsModel Model { get; set; }

        public async Task<ResultDto> ExecuteAsync()
        {
            var contact = await Model.CheckThenCreateAsync(contactRepository);
            var res = await sms.SendSmsAsync(Model.Body, contact);
            res.Data = Model;
            return res;
        }
    }
    public class SendSmsModel : ModelBase, IExternalIdModel, IMobileNumberModel, IContactIdModel
    {

        public string? ExternalId { get; set; }
        public int? ContactId { get; set; }
        public string Body { get; set; }
        public string? SourceName { get; set; }

        public override bool IsValid =>
            !(string.IsNullOrEmpty(MobileNumber) && string.IsNullOrEmpty(ExternalId) && !ContactId.HasValue);

        public string? MobileNumber { get; set; }

        [JsonIgnore]
        public MobileNumber? Mobile => MobileNumber.HasContent() ? new MobileNumber(MobileNumber) : null;
    }
}
