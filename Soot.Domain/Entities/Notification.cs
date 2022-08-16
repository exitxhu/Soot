using Soot.Domain.Base;

namespace Soot.Domain.Entities
{
    public partial class Notification : Root<Notification>
    {
        private readonly int DEFAULT_RETRY_COUNT = 3;

        public static Notification RawInctance() => new Notification();

        public void SendToContacts(IEnumerable<Contact> contacts, SendType sendType, DateTime? sendDate = null)
        {
            var acts = contacts.Select(n => new SendAction()
            {
                Notification = this,
                NotificationId = this.NotificaionId,
                Receiver = n,
                ReceiverId = n.ContactId,
                RetryCount = DEFAULT_RETRY_COUNT,
                SendDate = sendDate.HasValue ? sendDate.Value : DateTime.UtcNow,
                SendType = sendType
            });
            SendActions = acts;
        }

        public override Notification SetTrueId(object id)
        {
            NotificaionId = (int)id;
            return this;
        }

        public Notification(string body, IEnumerable<SendAction> sendActions = null)
        {
            Body = body;
            SendActions = sendActions;
        }
        public Notification()
        {

        }

        public long NotificaionId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; } = DateTime.UtcNow;

        public IEnumerable<SendAction> SendActions { get; set; }

        public class SendAction
        {

            public int SendActionId { get; set; }
            public long NotificationId { get; set; }
            public int ReceiverId { get; set; }
            public SendType SendType { get; set; }
            public DateTime SendDate { get; set; }
            public int RetryCount { get; set; }
            public bool IsDeliveryRequested { get; set; } = false;
            public Notification Notification { get; set; }
            public Contact Receiver { get; set; }
            public IEnumerable<SendResult> SendResults { get; set; }
        }
        public class SendResult
        {
            public int SendResultId { get; set; }
            public int SendActionId { get; set; }
            public string Details { get; set; }
            public SendResultType Result { get; set; }
            public bool IsRetry { get; set; }
            public DateTime ResultDate { get; set; }

            public SendAction SendAction { get; set; }

        }
        [Flags]
        public enum SendType
        {
            Sms = 1,
            EMAIL = 1 << 1,
            WEB = 1 << 2,

            ALL = Sms | EMAIL | WEB
        }
        public enum SendResultType
        {
            SENT_NO_DELIVERY = 1,
            SENT_DELIVERY_PENDING,
            SENT_DELIVERED,
            SENT_FAILED,
            CANCELED,
        }
    }
}
