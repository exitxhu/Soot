using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soot.Domain.Entities;

namespace Soot.Db.Ef.Configuration;

public class SendResultConfig : IEntityTypeConfiguration<Notification.SendResult>
{
    public void Configure(EntityTypeBuilder<Notification.SendResult> b)
    {
        b.HasKey(n => n.SendResultId);
        b.Property(n => n.Result).HasConversion<int>();
    }
}