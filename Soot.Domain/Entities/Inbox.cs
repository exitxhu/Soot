using Soot.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soot.Domain
{
    public partial class Inbox : Root<Inbox>
    {
        public static Inbox RawInstance => new();

        public int InboxId { get; set; }
        public int ContactId { get; set; }
        public DateTime CreateDate { get; } = DateTime.UtcNow;
        public string Description { get; set; }

        public Contact Contact { get; set; }
        public List<InboxItem> Items { get; set; }

        public class InboxItem
        {
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
