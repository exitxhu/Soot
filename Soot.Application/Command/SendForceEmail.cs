using Soot.Application.Module.Email;
using Soot.Application.Services;
using Soot.Domain.Entities;
using Soot.Domain.Repositories;
using Soot.Domain.Shared;

namespace Soot.Application.Command;

public class SendForceEmail : ISendForceEmail
{
    private readonly IEmailModule _email;
    private readonly IContactRepository _contactRepository;
    public SendForceEmail(IEmailModule email, IContactRepository contactRepository)
    {
        this._email = email;
        this._contactRepository = contactRepository;
    }

    public SendMailModel Model { get; set; }

    public async Task<ResultDto> ExecuteAsync()
    {
        var contact = await Model.CheckThenCreateAsync(_contactRepository);
        var res = await _email.SendEmailAsync(Model.Subject, Model.Body, contact);
        res.Data = Model;
        return res;
    }
}