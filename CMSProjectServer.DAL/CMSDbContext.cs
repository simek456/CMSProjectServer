﻿using CMSProjectServer.DAL.EntityBuilders;
using CMSProjectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMSProjectServer.DAL;

public class CMSDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ArticleTag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("TODO: ConnectionString", x => x.MigrationsAssembly("CMSProjectServer.DAL.Migrations"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArticleTagEntityBuilder());
        modelBuilder.ApplyConfiguration(new ArticleEntityBuilder());
        modelBuilder.ApplyConfiguration(new CommentEntityBuilder());
        modelBuilder.ApplyConfiguration(new LikeEntityBuilder());
        modelBuilder.ApplyConfiguration(new UserEntityBuilder());
    }
}