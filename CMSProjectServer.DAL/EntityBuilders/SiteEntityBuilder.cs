using CMSProjectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSProjectServer.DAL.EntityBuilders;

internal class SiteEntityBuilder : IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SiteContent)
            .HasColumnType("jsonb")
            .IsRequired();
    }
}