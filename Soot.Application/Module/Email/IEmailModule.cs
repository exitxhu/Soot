using Soot.Domain.Base;
using Soot.Domain.Entities;
using Soot.Domain.Shared;

namespace Soot.Application.Module.Email
{
    public interface IEmailModule : ISoot
    {
        Task<ResultDto> SendEmailAsync(string Body, Contact Receiver);
    }
}
