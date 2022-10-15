using Soot.Domain.Base;
using Soot.Domain.Entities;
using Soot.Domain.Shared;

namespace Soot.Application.Module.Sms
{
    public interface ISmsModule : ISoot
    {
        Task<ResultDto> SendLookupSmsAsync(string templateId, string token, string token2, string token3, Contact? contact);
        Task<ResultDto> SendSmsAsync(string body, Contact? contact);
    }

}
