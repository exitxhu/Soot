using Soot.Domain.Base;
using Soot.Domain.ValueObjects;

namespace Soot.Domain.Entities
{
    public partial class Contact : Root<Contact>
    {
        public static Contact RawInstance => new();
        public Contact(Inbox inbox, List<Notification> notifications, List<ContactTag> tags, List<ExternalContactMapping> externalMappings)
        {
            Inbox = inbox;
            Notifications = notifications;
            Tags = tags;
            ExternalMappings = externalMappings;
        }
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
            public ExternalContactMapping()
            {

            }
            public ExternalContactMapping(int externalContactId, int contactId, string? externalId, string? externalSourceName, Contact contact)
            {
                ExternalContactId = externalContactId;
                ContactId = contactId;
                ExternalId = externalId;
                ExternalSourceName = externalSourceName;
                Contact = contact;
            }

            public int ExternalContactId { get; set; }
            public int ContactId { get; set; }
            public string? ExternalId { get; set; }
            public string? ExternalSourceName { get; set; }
            public Contact Contact { get; set; }
        }
        public class ContactTag
        {
            public ContactTag()
            {

            }
            public ContactTag(int contactTagId, int contactId, string tag, Contact contact)
            {
                ContactTagId = contactTagId;
                ContactId = contactId;
                Tag = tag;
                Contact = contact;
            }

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
        public record ContactExternalInfoDto(string ExternalId, string SourceName);
        public record ContactDto(int ContactId, string? Name, EmailAddress EmailAddress, MobileNumber MobileNumber, Uri WebSocket);

    }
}
