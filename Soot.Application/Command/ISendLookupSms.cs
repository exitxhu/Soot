using Soot.Domain.Shared;

namespace Soot.Application.Command
{
    public interface ISendLookupSms
    {
        Task<ResultDto> ExecuteAsync();
        public SendLookupSmsModel Model { get; set; }

    }
}
