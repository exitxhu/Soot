using Soot.Domain.Shared;

namespace Soot.Application.Command
{
    public interface IDeleteContacts
    {
        Task<ResultDto> ExecuteAsync();
        public List<DeleteContactModel> Model { get; set; }

    }
}
