using Kavenegar_NetCore_unofficial_;
using Soot.Domain.Shared;
using Soot.Application.Module.Sms;
using Soot.Domain.Entities;

namespace Soot.Sms.Kavenegar
{
    public class KavenegarSmsModule : ISmsModule
    {
        private readonly KavenegarService _kvsrc;

        public KavenegarSmsModule(KavenegarService kvsrc)
        {
            this._kvsrc = kvsrc;
        }

        public async Task<ResultDto> SendLookupSmsAsync(string templateId, string token, string token2, string token3, Contact? contact)
        {
            if (contact?.MobileNumber is null) 
                throw new InvalidDataException("mobile not found");
            var resp = await _kvsrc.Lookup(new string[] { contact.MobileNumber.ToString() }, templateId, token, token2, token3);
            if (resp.Return.status == 200)
                return new ResultDto
                {
                    Data = resp.entries,
                    IsSuccessful = true
                };
            else return new ResultDto { IsSuccessful = false, Message = resp.Return.message, Data = resp.entries };
        }

        public async Task<ResultDto> SendSmsAsync(string body, Contact? contact)
        {
            if (contact?.MobileNumber is null) 
                throw new InvalidDataException("mobile not found");
            var resp = await _kvsrc.Send(new string[] { contact.MobileNumber.ToString() }, body);
            if (resp.Return.status == 200)
                return new ResultDto
                {
                    Data = resp.entries,
                    IsSuccessful = true
                };
            else return new ResultDto { IsSuccessful = false, Message = resp.Return.message, Data = resp.entries };
        }
    }
}
