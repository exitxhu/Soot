using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soot.Domain.Entities;

namespace Soot.Db.Ef.Configuration;

public class InboxItemActionsConfig : IEntityTypeConfiguration<Inbox.InboxItemActions>
{
    public void Configure(EntityTypeBuilder<Inbox.InboxItemActions> builder)
    {
        builder.HasKey(n => n.InboxItemActionId);
    }
}