using CMSProjectServer.DAL.EntityBuilders;
using CMSProjectServer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMSProjectServer.DAL;

public class CMSDbContext : IdentityDbContext<User>
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ArticleTag> Tags { get; set; }
    public DbSet<Site> CurrentSites { get; set; }
    public DbSet<Site> HistoricSites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=CMSProjectDatabase;Username=CMSProjectServer;Password=CMSProjectServer", x => x.MigrationsAssembly("CMSProjectServer.DAL.Migrations"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ArticleTagEntityBuilder());
        modelBuilder.ApplyConfiguration(new ArticleEntityBuilder());
        modelBuilder.ApplyConfiguration(new CommentEntityBuilder());
        modelBuilder.ApplyConfiguration(new LikeEntityBuilder());
        modelBuilder.ApplyConfiguration(new UserEntityBuilder());
        modelBuilder.ApplyConfiguration(new SiteEntityBuilder());
    }
}