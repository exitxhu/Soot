using Soot.Application.Exceptions;
using Soot.Application.Services;
using Soot.Domain.Entities;
using Soot.Domain.Repositories;
using Soot.Domain.Shared;

namespace Soot.Application.Command;

public class UpsertContacts : IUpsertContacts
{
    private readonly IContactRepository _contactRepository;

    public UpsertContacts(IContactRepository contactRepository)
    {
        this._contactRepository = contactRepository;
    }

    public List<UpsertContactModel> Model { get; set; }

    public async Task<ResultDto> ExecuteAsync()
    {
        var res = new ResultDto();
        try
        {
            InvalidModelException.ThrowIfInvalid(Model);
            var sourceName = Model.FirstOrDefault()?.SourceName;
            var check = await _contactRepository.GetAllByExternalIdAsync(Model.Select(n => n.ExternalId), Model.FirstOrDefault()?.SourceName);
            var news = Model
                .Where(n => !check.Any(a => a.ExternalMappings.Any(n => n.ExternalSourceName == sourceName)))
                .Select(n => new Contact
                {
                    Name = n.Name,
                    EmailAddress = n.Email,
                    ExternalMappings = new List<Contact.ExternalContactMapping>
                        {new Contact.ExternalContactMapping{ExternalId = n.ExternalId,ExternalSourceName = sourceName,}},
                    MobileNumber = n.Mobile,
                    WebSocket = n.WebSocket,
                    Tags = n.Tags.Select(n => new Contact.ContactTag { Tag = n }).ToList()
                }).ToList();
            check.ForEach(n =>
            {
                var temp = Model
                    .FirstOrDefault(m => n.ExternalMappings.Any(x => m.ExternalId == x.ExternalId && x.ExternalSourceName.Equals(m.SourceName, StringComparison.InvariantCultureIgnoreCase)));
                n.Merge(temp);
            });
            _contactRepository.AddRange(news);
            _contactRepository.UpdateRange(check!);
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