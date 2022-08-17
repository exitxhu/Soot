using Soot.Domain.Base;

namespace Soot.Domain.Entities
{
    public partial class Inbox : Root<Inbox>
    {
        public Inbox()
        {

        }
        public Inbox(int inboxId, int contactId, string description, Contact contact, List<InboxItem> items)
        {
            InboxId = inboxId;
            ContactId = contactId;
            Description = description;
            Contact = contact;
            Items = items;
        }

        public static Inbox RawInstance => new();

        public int InboxId { get; set; }
        public int ContactId { get; set; }
        public DateTime CreateDate { get; } = DateTime.UtcNow;
        public string Description { get; set; }

        public Contact Contact { get; set; }
        public List<InboxItem> Items { get; set; }

        public class InboxItem
        {
            public InboxItem()
            {

            }
            public InboxItem(int inboxItemId, long notificationId, int inboxId, Inbox inbox, InboxItemStatus status, Notification notification, List<InboxItemActions> inboxItemActions)
            {
                InboxItemId = inboxItemId;
                NotificationId = notificationId;
                InboxId = inboxId;
                Inbox = inbox;
                Status = status;
                Notification = notification;
                InboxItemActions = inboxItemActions;
            }

            public void ChangeItemStatus(InboxItemStatus newState, string details)
            {
                if (newState == Status)
                    return;
                Status = newState;
                InboxItemActions ??= new List<InboxItemActions>();
                InboxItemActions.Add(new Inbox.InboxItemActions
                {
                    ActionDate = DateTime.UtcNow,
                    ActionType = newState,
                    Details = details,
                });
            }
            public int InboxItemId { get; set; }
            public long NotificationId { get; set; }
            public int InboxId { get; set; }
            public Inbox Inbox { get; set; }
            public InboxItemStatus Status { get; set; }
            public Notification Notification { get; set; }
            public List<InboxItemActions> InboxItemActions { get; set; }

        }
        public class InboxItemActions
        {
            public InboxItemActions()
            {

            }
            public InboxItemActions(int inboxItemActionId, int inboxItemId, InboxItemStatus actionType, DateTime actionDate, string details, InboxItem inboxItem)
            {
                InboxItemActionId = inboxItemActionId;
                InboxItemId = inboxItemId;
                ActionType = actionType;
                ActionDate = actionDate;
                Details = details;
                InboxItem = inboxItem;
            }

            public int InboxItemActionId { get; set; }
            public int InboxItemId { get; set; }
            public InboxItemStatus ActionType { get; set; }
            public DateTime ActionDate { get; set; }
            public string Details { get; set; }
            public InboxItem InboxItem { get; set; }

        }
        public enum InboxItemStatus
        {
            NEW = 1,
            SHOWN = 2,
            SEAN = 3
        }

        public override Inbox SetTrueId(object id)
        {
            InboxId = (int)id;
            return this;
        }
    }
}
