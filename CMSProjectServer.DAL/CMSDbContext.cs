using CMSProjectServer.DAL.EntityBuilders;
using CMSProjectServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CMSProjectServer.DAL;

public class CMSDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration configuration;

    public DbSet<Article> Articles { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ArticleCategory> Categories { get; set; }
    public DbSet<Site> CurrentSites { get; set; }
    public DbSet<OldSite> HistoricSites { get; set; }

    public CMSDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration["DB_CONNECTION_STRING"], x => x.MigrationsAssembly("CMSProjectServer.DAL.Migrations"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ArticleCategoryEntityBuilder());
        modelBuilder.ApplyConfiguration(new ArticleEntityBuilder());
        modelBuilder.ApplyConfiguration(new CommentEntityBuilder());
        modelBuilder.ApplyConfiguration(new LikeEntityBuilder());
        modelBuilder.ApplyConfiguration(new UserEntityBuilder());
        modelBuilder.ApplyConfiguration(new SiteEntityBuilder());
        modelBuilder.ApplyConfiguration(new OldSiteEntityBuilder());
        SeedInitialAdmin(modelBuilder);
    }

    private static void SeedInitialAdmin(ModelBuilder modelBuilder)
    {
        const string ADMIN_ID = "b7ad606a-2f3d-4ff5-89f4-278100d10b85";
        const string ROLE_ID = "cf86c6c1-6dc9-4247-bbf4-4bf0dc31a345";
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN",
            Id = ROLE_ID,
            ConcurrencyStamp = ROLE_ID
        });
        var hasher = new PasswordHasher<IdentityUser>();
        var user = new User()
        {
            Id = ADMIN_ID,
            Email = "admin@admin.com",
            Name = "FirstAdmin",
            UserName = "Admin",
            NormalizedUserName = "ADMIN"
        };
        user.PasswordHash = hasher.HashPassword(user, "Password#=123");
        modelBuilder.Entity<User>().HasData(user);
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
        {
            UserId = ADMIN_ID,
            RoleId = ROLE_ID,
        });
    }
}