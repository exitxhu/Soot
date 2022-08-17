using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soot.Domain.Entities;

namespace Soot.Db.Ef.Configuration
{
    public class InboxConfig : IEntityTypeConfiguration<Inbox>
    {
        public void Configure(EntityTypeBuilder<Inbox> builder)
        {
            builder.HasKey(n => n.InboxId);
            builder.HasOne(n => n.Contact).WithOne(n => n.Inbox).HasForeignKey<Inbox>(n => n.ContactId);
        }
    }
}
