using Soot.Domain.Base;

namespace Soot.Domain.Entities
{
    public partial class Notification : Root<Notification>
    {
        private const int DefaultRetryCount = 3;

        public static Notification RawInstance() => new Notification();

        public void SendToContacts(IEnumerable<Contact> contacts, SendType sendType, DateTime? sendDate = null)
        {
            var acts = contacts.Select(n => new SendAction()
            {
                Notification = this,
                NotificationId = this.NotificationId,
                Receiver = n,
                ReceiverId = n.ContactId,
                RetryCount = DefaultRetryCount,
                SendDate = sendDate ?? DateTime.UtcNow,
                SendType = sendType
            });
            SendActions = acts;
        }

        public override Notification SetTrueId(object id)
        {
            NotificationId = (int)id;
            return this;
        }

        public Notification(string body, IEnumerable<SendAction>? sendActions = null)
        {
            Body = body;
            SendActions = sendActions;
        }
        public Notification()
        {

        }

        public long NotificationId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; } = DateTime.UtcNow;
        public IEnumerable<SendAction>? SendActions { get; set; }
        public class SendAction
        {
            public SendAction()
            {
                
            }
            public SendAction(int sendActionId, long notificationId, int receiverId, SendType sendType, DateTime sendDate, int retryCount, bool isDeliveryRequested, Notification notification, Contact receiver, IEnumerable<SendResult> sendResults)
            {
                SendActionId = sendActionId;
                NotificationId = notificationId;
                ReceiverId = receiverId;
                SendType = sendType;
                SendDate = sendDate;
                RetryCount = retryCount;
                IsDeliveryRequested = isDeliveryRequested;
                Notification = notification;
                Receiver = receiver;
                SendResults = sendResults;
            }

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
            public SendResult()
            {
                
            }
            public SendResult(int sendResultId, int sendActionId, string details, SendResultType result, bool isRetry, DateTime resultDate, SendAction sendAction)
            {
                SendResultId = sendResultId;
                SendActionId = sendActionId;
                Details = details;
                Result = result;
                IsRetry = isRetry;
                ResultDate = resultDate;
                SendAction = sendAction;
            }
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
            SMS = 1,
            EMAIL = 1 << 1,
            WEB = 1 << 2,
            ALL = SMS | EMAIL | WEB
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
