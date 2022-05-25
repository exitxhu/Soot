using Soot.Domain;
using Soot.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soot.Application.Sms
{
    public interface ISmsModule : ISoot
    {
        Task<ResultDto> SendSmsAsync(string body, Contact contact);
    }

}
