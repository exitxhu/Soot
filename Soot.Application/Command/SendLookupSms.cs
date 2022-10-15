using Soot.Application.Module.Sms;
using Soot.Application.Services;
using Soot.Domain.Repositories;
using Soot.Domain.Shared;

namespace Soot.Application.Command;

public class SendLookupSms : ISendLookupSms
{
    private readonly ISmsModule _sms;
    private readonly IContactRepository _contactRepository;
    public SendLookupSms(ISmsModule sms, IContactRepository contactRepository)
    {
        this._sms = sms;
        this._contactRepository = contactRepository;
    }
    public SendLookupSmsModel Model { get; set; }

    public async Task<ResultDto> ExecuteAsync()
    {
        var contact = await Model.CheckThenCreateAsync(_contactRepository);
        ResultDto res = await _sms.SendLookupSmsAsync(Model.TemplateId, Model.Token, Model.Token2, Model.Token3, contact);
        res.Data = Model;
        return res;
    }
}