using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soot.Domain.Entities;
using Soot.Domain.ValueObjects;

namespace Soot.Db.Ef.Configuration
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> b)
        {
            b.HasKey(n => n.ContactId);
            b.Property(n => n.MobileNumber).HasConversion(n => n!.ToString(), n => new MobileNumber(n))
                .HasMaxLength(20);
            b.Property(n => n.EmailAddress).HasConversion(n => n!.ToString(), n => new EmailAddress(n))
                .HasMaxLength(500);
            b.Property(n => n.WebSocket).HasConversion(n => n!.ToString(), n => new Uri(n))
                .HasMaxLength(500);
        }
    }
}
