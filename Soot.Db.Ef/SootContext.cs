using Microsoft.EntityFrameworkCore;
using Soot.Domain.Entities;

namespace Soot.Db.Ef
{
    public class SootContext : DbContext
    {
        public SootContext(DbContextOptions<SootContext> op) : base(op)
        {

        }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Notification.SendAction> SendActions { get; set; }
        public DbSet<Notification.SendResult> SendResults { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Contact.ExternalContactMapping> ExternalContactMappings { get; set; }
        public DbSet<Contact.ContactTag> ContactTags{ get; set; }
        public DbSet<Inbox> Inbox { get; set; }
        public DbSet<Inbox.InboxItem> InboxItems { get; set; }
        public DbSet<Inbox.InboxItemActions> InboxItemActions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.HasDefaultSchema("Soot");
            mb.ApplyConfigurationsFromAssembly(typeof(SootContext).Assembly);
        }
    }
}
