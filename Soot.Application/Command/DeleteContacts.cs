using Soot.Application.Exceptions;
using Soot.Domain.Repositories;
using Soot.Domain.Shared;

namespace Soot.Application.Command;

public class DeleteContacts : IDeleteContacts
{
    private readonly IContactRepository _contactRepository;
    public DeleteContacts(IContactRepository contactRepository)
    {
        this._contactRepository = contactRepository;
    }
    public List<DeleteContactModel> Model { get; set; }
    public async Task<ResultDto> ExecuteAsync()
    {
        InvalidModelException.ThrowIfInvalid(Model);
        var res = new ResultDto();
        try
        {
            var ids = Model.Select(n => n.ExternalId);
            var src = Model?.First()?.SourceName;
            var all = await _contactRepository.GetAllByExternalIdAsync(ids!, src);
            _contactRepository.DeleteRange(all!);
            await _contactRepository.SaveAsync();
            res.IsSuccessful = true;
            return res;

        }
        catch (Exception ex)
        {
            res.IsSuccessful = false;
            res.Data = ex;
            res.Message = ex.Message;
            return res;
        }
    }
}