using Soot.Domain.Base;
using Soot.Domain.Entities;
using Soot.Domain.Shared;

namespace Soot.Application.Module.Sms
{
    public interface ISmsModule : ISoot
    {
        Task<ResultDto> SendSmsAsync(string body, Contact contact);
    }

}
