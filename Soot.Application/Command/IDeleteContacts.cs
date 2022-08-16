using linqPlusPlus;
using Soot.Application.Base;
using Soot.Application.Services;
using Soot.Domain;
using Soot.Domain.Repositories;
using Soot.Domain.Shared;
using System.Text.Json.Serialization;
using Soot.Application.Exceptions;

namespace Soot.Application.Command
{
    public interface IDeleteContacts
    {
        Task<ResultDto> ExecuteAsync();
        public List<DeleteContactModel> Model { get; set; }

    }
    public class DeleteContacts : IDeleteContacts
    {
        private readonly IContactRepository contactRepository;

        public DeleteContacts(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public List<DeleteContactModel> Model { get; set; }

        public async Task<ResultDto> ExecuteAsync()
        {
            InvalidModelException.ThrowIfInvalid(Model);
            var res = new ResultDto();
            try
            {

                var ids = Model.Select(n => n.ExternalId);
                var src = Model.First().SourceName;
                var alls = await contactRepository.GetAllByExternalIdAsync(ids, src);
                contactRepository.DeleteRange(alls);
                await contactRepository.SaveAsync();
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
    public class DeleteContactModel : ModelBase, IExternalIdModel
    {
        public override bool IsValid =>
            !(!ExternalId.HasContent() && !SourceName.HasContent());
        public string? ExternalId { get; set; }
        public string? SourceName { get; set; }
    }

}
