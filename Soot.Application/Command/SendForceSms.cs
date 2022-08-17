using Soot.Application.Module.Sms;
using Soot.Application.Services;
using Soot.Domain.Entities;
using Soot.Domain.Repositories;
using Soot.Domain.Shared;

namespace Soot.Application.Command;

public class SendForceSms : ISendForceSms
{
    private readonly ISmsModule _sms;
    private readonly IContactRepository _contactRepository;
    public SendForceSms(ISmsModule sms, IContactRepository contactRepository)
    {
        this._sms = sms;
        this._contactRepository = contactRepository;
    }
    public SendSmsModel Model { get; set; }
    public async Task<ResultDto> ExecuteAsync()
    {
        var contact = await Model.CheckThenCreateAsync(_contactRepository);
        var res = await _sms.SendSmsAsync(Model.Body, contact);
        res.Data = Model;
        return res;
    }
}