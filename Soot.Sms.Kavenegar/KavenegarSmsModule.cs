using Kavenegar_NetCore_unofficial_;
using Soot.Domain;
using Soot.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soot.Application.Module.Sms;
using Soot.Domain.Entities;

namespace Soot.Sms.Kavenegar
{
    public class KavenegarSmsModule : ISmsModule
    {
        private readonly KavenegarService kvsrc;

        public KavenegarSmsModule(KavenegarService kvsrc)
        {
            this.kvsrc = kvsrc;
        }
        public async Task<ResultDto> SendSmsAsync(string body, Contact contact)
        {
            var resp = await kvsrc.Send(new string[] { contact.MobileNumber.ToString() }, body);
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
