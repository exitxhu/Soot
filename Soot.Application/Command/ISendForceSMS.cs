using Soot.Domain.Shared;

namespace Soot.Application.Command
{
    public interface ISendForceSms
    {
        Task<ResultDto> ExecuteAsync();
        public SendSmsModel Model { get; set; }

    }
}
