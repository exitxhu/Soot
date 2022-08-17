using Soot.Domain.Shared;

namespace Soot.Application.Command
{
    public interface ISendForceEmail
    {
        Task<ResultDto> ExecuteAsync();
        public SendMailModel Model { get; set; }

    }
}
