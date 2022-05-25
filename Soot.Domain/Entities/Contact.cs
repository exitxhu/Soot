
using Soot.Domain.Base;
using static Soot.Domain.Tag;

namespace Soot.Domain
{
    public partial class Contact : Root<Contact>
    {
        public static Contact RawInstance => new();
        public Contact()
        {

        }
        public int ContactId { get; set; }
        public int? InboxId { get; set; }
        public string? Name { get; set; }
        public EmailAddress? EmailAddress { get; set; }
        public MobileNumber? MobileNumber { get; set; }
        public Uri? WebSocket { get; set; }

        public Inbox Inbox { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<ContactTag> Tags { get; set; }
        public List<ExternalContactMapping> ExternalMappings { get; set; }
        public class ExternalContactMapping
        {
            public int ExternalContactId { get; set; }
            public int ContactId { get; set; }
            public string ExternalId { get; set; }
            public string ExternalSourceName { get; set; }

            public Contact Contact { get; set; }
        }
        public class ContactTag
        {
            public int ContactTagId { get; set; }
            public int ContactId { get; set; }
            public string Tag { get; set; }
            public Contact Contact { get; set; }
        }

        public override Contact SetTrueId(object id)
        {
            ContactId = (int)id;
            return this;
        }


        public record ContactExtenalInfoDto(string ExternalId, string SourceName);
        public record ContactDto(int ContactId, string? Name, EmailAddress? EmailAddress, MobileNumber? MobileNumber, Uri? WebSocket);

    }
    public static class ContactExtensions
    {
        public static Contact.ContactDto ToContactDto(this Contact contact) => new Contact.ContactDto(contact.ContactId, contact.Name, contact.EmailAddress, contact.MobileNumber, contact.WebSocket);
    }
}
