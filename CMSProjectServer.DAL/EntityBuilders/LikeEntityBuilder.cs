using CMSProjectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSProjectServer.DAL.EntityBuilders;

internal class LikeEntityBuilder : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ArticleId });
    }
}