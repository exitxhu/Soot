using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soot.Domain.Entities;

namespace Soot.Db.Ef.Configuration;

public class SendActionConfig : IEntityTypeConfiguration<Notification.SendAction>
{
    public void Configure(EntityTypeBuilder<Notification.SendAction> b)
    {
        b.HasKey(n => n.SendActionId);
        b.Property(n => n.SendType).HasConversion<int>();
    }
}