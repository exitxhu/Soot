using linqPlusPlus;
using Soot.Application.Base;
using Soot.Application.Command;
using Soot.Domain;
using Soot.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soot.Domain.Entities;
using Soot.Domain.ValueObjects;
using static Soot.Domain.Entities.Contact;

namespace Soot.Application.Services
{
    public class ContactService
    {
        private readonly IContactRepository contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }


    }
    public static class ContactServiceExtensions
    {
        private static readonly string DEFAULT_SOURCE_NAME = "Unknown";

        public static async Task<Contact> CheckThenCreateAsync(this SendMailModel model, IContactRepository repo)
        {
            var t = model.CheckByContactId(repo) ?? model.CheckByExternalId(repo) ?? model.CheckByEmailAddress(repo) ?? await model.CreateContact(repo);
            return t;
        }
        public static async Task<Contact> CheckThenCreateAsync(this SendSmsModel model, IContactRepository repo)
        {
            var t = model.CheckByContactId(repo) ?? model.CheckByExternalId(repo) ?? model.CheckByMobileNumber(repo) ?? await model.CreateContact(repo);
            return t;
        }
        public static Contact Merge(this Contact contact, UpsertContactModel upd)
        {
            contact.WebSocket = upd.WebSocket;
            contact.EmailAddress = upd.Email;
            contact.MobileNumber = upd.Mobile;
            contact.Name = upd.Name;
            if (contact.Tags is null)
            {
                contact.Tags = upd.Tags.Select(n => new ContactTag { Tag = n }).ToList();
            }
            else
            {
                contact.Tags.AddRange(
                upd.Tags.ExceptBy(contact.Tags.Select(n => n.Tag), n => n).Select(n => new ContactTag { Tag = n }));
            }
            var mapping = contact.ExternalMappings.SingleOrDefault(n => n.ExternalSourceName.Equals(upd.SourceName, StringComparison.InvariantCultureIgnoreCase));
            if (mapping is null)
            {
                mapping = new Contact.ExternalContactMapping
                {
                    ExternalId = upd.ExternalId,
                    ExternalSourceName = upd.SourceName,

                };
                contact.ExternalMappings ??= new List<Contact.ExternalContactMapping>();
                contact.ExternalMappings.Add(mapping);
            }
            return contact;
        }
        static Contact? CheckByContactId(this IContactIdModel model, IContactRepository repo) =>
            model.ContactId.HasValue
                ? repo.GetByIdAsync(model.ContactId).Result
                : null;
        static Contact? CheckByExternalId(this IExternalIdModel model, IContactRepository repo) =>
            !string.IsNullOrEmpty(model.ExternalId)
                ? repo.GetByExternalIdAsync(model.ExternalId, model.SourceName).Result
                : null;
        static Contact? CheckByMobileNumber(this IMobileNumberModel model, IContactRepository repo) =>
            !string.IsNullOrEmpty(model.MobileNumber)
                ? repo.GetByMobileNumberAsync(model.Mobile).Result
                : null;
        static Contact? CheckByEmailAddress(this IEmailAddressModel model, IContactRepository repo) =>
            !string.IsNullOrEmpty(model.MailAddress)
                ? repo.GetByEmailAddressAsync(model.Email).Result
                : null;
        static async Task<Contact?> CreateContact(this SendMailModel model, IContactRepository repo)
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
                    ExternalSourceName = model.SourceName ?? DEFAULT_SOURCE_NAME
                });
            }
            repo.Add(c);
            await repo.SaveAsync();
            return c;
        }
        static async Task<Contact?> CreateContact(this SendSmsModel model, IContactRepository repo)
        {
            var c = Contact.RawInstance;
            if (model.MobileNumber.HasContent())
                c.MobileNumber = new MobileNumber(model.MobileNumber);
            if (model.ExternalId.HasContent())
            {
                c.ExternalMappings ??= new();
                c.ExternalMappings.Add(new Contact.ExternalContactMapping
                {
                    ExternalId = model?.ExternalId,
                    ExternalSourceName = model.SourceName ?? DEFAULT_SOURCE_NAME
                });
            }
            repo.Add(c);
            await repo.SaveAsync();
            return c;
        }
    }
}
