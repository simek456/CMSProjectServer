using CMSProjectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSProjectServer.DAL.EntityBuilders;

internal class OldSiteEntityBuilder : IEntityTypeConfiguration<OldSite>
{
    public void Configure(EntityTypeBuilder<OldSite> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SiteContent)
            .HasColumnType("jsonb")
            .IsRequired();
        builder.HasIndex(x => x.Name);
    }
}