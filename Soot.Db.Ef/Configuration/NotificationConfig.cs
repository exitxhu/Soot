using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soot.Domain;
namespace Soot.Db.Ef.Configuration
{
    public class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> b)
        {
            b.HasKey(n => n.NotificaionId);
        }
    }
    public class SendActionConfig : IEntityTypeConfiguration<Notification.SendAction>
    {
        public void Configure(EntityTypeBuilder<Notification.SendAction> b)
        {
            b.HasKey(n => n.SendActionId);
            b.Property(n => n.SendType).HasConversion<int>();
        }
    }
    public class SendResultConfig : IEntityTypeConfiguration<Notification.SendResult>
    {
        public void Configure(EntityTypeBuilder<Notification.SendResult> b)
        {
            b.HasKey(n => n.SendResultId);
            b.Property(n => n.Result).HasConversion<int>();
        }
    }
}
