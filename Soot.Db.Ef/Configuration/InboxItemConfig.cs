using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soot.Domain.Entities;

namespace Soot.Db.Ef.Configuration;

public class InboxItemConfig : IEntityTypeConfiguration<Inbox.InboxItem>
{
    public void Configure(EntityTypeBuilder<Inbox.InboxItem> builder)
    {
        builder.HasKey(n => n.InboxItemId);
    }
}