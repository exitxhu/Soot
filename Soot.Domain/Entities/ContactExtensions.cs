namespace Soot.Domain.Entities;

public static class ContactExtensions
{
    public static Contact.ContactDto ToContactDto(this Contact contact) => new Contact.ContactDto(contact.ContactId, contact.Name, contact.EmailAddress, contact.MobileNumber, contact.WebSocket);
}