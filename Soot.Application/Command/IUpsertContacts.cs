using Soot.Domain.Shared;

namespace Soot.Application.Command
{
    public interface IUpsertContacts
    {
        Task<ResultDto> ExecuteAsync();
        public List<UpsertContactModel> Model { get; set; }

    }
}
