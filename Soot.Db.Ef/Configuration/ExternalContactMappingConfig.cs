using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soot.Domain.Entities;

namespace Soot.Db.Ef.Configuration;

public class ExternalContactMappingConfig : IEntityTypeConfiguration<Contact.ExternalContactMapping>
{
    public void Configure(EntityTypeBuilder<Contact.ExternalContactMapping> builder)
    {
        builder.HasKey(n => n.ExternalContactId);
    }
}