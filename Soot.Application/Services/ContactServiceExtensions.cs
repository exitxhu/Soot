using linqPlusPlus;
using Soot.Application.Base;
using Soot.Application.Command;
using Soot.Domain.Entities;
using Soot.Domain.Repositories;
using Soot.Domain.ValueObjects;

namespace Soot.Application.Services;

public static class ContactServiceExtensions
{
    private const string DefaultSourceName = "Unknown";
    public static async Task<Contact> CheckThenCreateAsync(this SendMailModel model, IContactRepository repo)
    {
        return model.CheckByContactId(repo) ?? 
               model.CheckByExternalId(repo) ?? 
               model.CheckByEmailAddress(repo) ?? 
               await model.CreateContact(repo);
    }
    public static async Task<Contact?> CheckThenCreateAsync(this SendSmsModel model, IContactRepository repo)
    {
        return model.CheckByContactId(repo) ?? 
               model.CheckByExternalId(repo) ?? 
               model.CheckByMobileNumber(repo) ?? 
               await model.CreateContact(repo);
    }
    public static Contact Merge(this Contact contact, UpsertContactModel upd)
    {
        contact.WebSocket = upd.WebSocket;
        contact.EmailAddress = upd.Email;
        contact.MobileNumber = upd.Mobile;
        contact.Name = upd.Name;
        contact.Tags.AddRange(
            upd.Tags.ExceptBy(contact.Tags.Select(n => n.Tag), n => n).Select(n => new Contact.ContactTag { Tag = n }));
        var mapping = contact.ExternalMappings.SingleOrDefault(n => n.ExternalSourceName != null && n.ExternalSourceName.Equals(upd.SourceName, StringComparison.InvariantCultureIgnoreCase));
        if (mapping is not null) return contact;
        mapping = new Contact.ExternalContactMapping
        {
            ExternalId = upd.ExternalId,
            ExternalSourceName = upd.SourceName,

        };
        contact.ExternalMappings ??= new List<Contact.ExternalContactMapping>();
        contact.ExternalMappings.Add(mapping);
        return contact;
    }

    private static Contact? CheckByContactId(this IContactIdModel model, IContactRepository repo) =>
        model.ContactId.HasValue
            ? repo.GetByIdAsync(model.ContactId).Result
            : null;

    private static Contact? CheckByExternalId(this IExternalIdModel model, IContactRepository repo) =>
        !string.IsNullOrEmpty(model.ExternalId)
            ? repo.GetByExternalIdAsync(model.ExternalId, model.SourceName).Result
            : null;

    private static Contact? CheckByMobileNumber(this IMobileNumberModel model, IContactRepository repo) =>
        !string.IsNullOrEmpty(model.MobileNumber)
            ? repo.GetByMobileNumberAsync(model.Mobile).Result
            : null;

    private static Contact? CheckByEmailAddress(this IEmailAddressModel model, IContactRepository repo) =>
        !string.IsNullOrEmpty(model.MailAddress)
            ? repo.GetByEmailAddressAsync(model.Email).Result
            : null;

    private static async Task<Contact> CreateContact(this SendMailModel model, IContactRepository repo)
    {
        var c = Contact.RawInstance;
        if (!string.IsNullOrEmpty(model.MailAddress))
            c.EmailAddress = new EmailAddress(model.MailAddress);
        if (!string.IsNullOrEmpty(model.ExternalId))
        {
            c.ExternalMappings ??= new();
            c.ExternalMappings.Add(new Contact.ExternalContactMapping
            {
                ExternalId = model.ExternalId,
                ExternalSourceName = model.SourceName ?? DefaultSourceName
            });
        }
        repo.Add(c);
        await repo.SaveAsync();
        return c;
    }
    private static async Task<Contact?> CreateContact(this SendSmsModel model, IContactRepository repo)
    {
        var c = Contact.RawInstance;
        if (model.MobileNumber.HasContent())
            c.MobileNumber = new MobileNumber(model.MobileNumber);
        if (model.ExternalId.HasContent())
        {
            c.ExternalMappings ??= new List<Contact.ExternalContactMapping>();
            c.ExternalMappings.Add(new Contact.ExternalContactMapping
            {
                ExternalId = model?.ExternalId,
                ExternalSourceName = model?.SourceName ?? DefaultSourceName
            });
        }
        repo.Add(c);
        await repo.SaveAsync();
        return c;
    }
}