using CMSProjectServer.DAL;
using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using CMSProjectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

internal class ArticleService : IArticleService
{
    private readonly CMSDbContext dbContext;

    public ArticleService(CMSDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<ArticleDto>> GetArticleById(int id)
    {
        var result = await dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);
        if (result == null)
        {
            return Result<ArticleDto>.Failure();
        }
        return new ArticleDto()
        {
            Categories = result.Tags.Select(x => x.Tag).ToList(),
            Contents = result.Contents,
            Description = result.Description,
            Title = result.Title,
            Id = id
        };
    }

    public async Task<Result<CreateArticleResponseDto>> CreateArticle(ArticleDto articleDto, string authorUsername)
    {
        if (articleDto is null)
        {
            return Result<CreateArticleResponseDto>.Failure("Incorrect Format");
        }
        var author = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == authorUsername);
        if (author == null)
        {
            return Result<CreateArticleResponseDto>.Failure("User doesn't exist");
        }
        var newArticle = new Article();
        newArticle.Title = articleDto.Title;
        newArticle.Contents = articleDto.Contents;
        newArticle.Description = articleDto.Description;
        newArticle.CreatedAt = DateTime.UtcNow;
        newArticle.Author = author;

        dbContext.Add(newArticle);

        var existingTags = await dbContext.Tags.Where(x => articleDto.Categories.Contains(x.Tag)).ToListAsync();
        foreach (var newTag in articleDto.Categories.Where(x => existingTags.Any(e => e.Tag == x) == false))
        {
            var tag = new ArticleTag() { Tag = newTag };
            newArticle.Tags.Add(tag);
        }
        foreach (var existingTag in existingTags)
        {
            newArticle.Tags.Add(existingTag);
        }

        await dbContext.SaveChangesAsync();

        return new CreateArticleResponseDto() { Id = newArticle.Id };
    }

    public async Task<Result<CreateArticleResponseDto>> UpdateArticle(ArticleDto articleDto, string authorUsername)
    {
        var editedArticle = await dbContext.Articles.Include(x => x.Tags).Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == articleDto.Id);
        if (editedArticle == null)
        {
            return Result<CreateArticleResponseDto>.Failure("Article doen't exist");
        }
        if (editedArticle.Author.UserName != authorUsername)
        {
            return Result<CreateArticleResponseDto>.Failure("You are not the author");
        }

        editedArticle.Title = articleDto.Title;
        editedArticle.Contents = articleDto.Contents;
        editedArticle.Description = articleDto.Description;
        editedArticle.UpdatedAt = DateTime.UtcNow;
        editedArticle.Tags.RemoveAll(x => articleDto.Categories.Contains(x.Tag) == false);
        var tagsToAdd = articleDto.Categories.Where(x => editedArticle.Tags.Any(e => e.Tag == x) == false).ToList();

        var existingTags = await dbContext.Tags.Where(x => tagsToAdd.Contains(x.Tag)).ToListAsync();
        foreach (var newTag in tagsToAdd.Where(x => existingTags.Any(e => e.Tag == x) == false))
        {
            var tag = new ArticleTag() { Tag = newTag };
            tag.Article.Add(editedArticle);
            editedArticle.Tags.Add(tag);
            dbContext.Tags.Add(tag);
        }
        foreach (var existingTag in existingTags)
        {
            existingTag.Article.Add(editedArticle);
            editedArticle.Tags.Add(existingTag);
        }

        await dbContext.SaveChangesAsync();

        return new CreateArticleResponseDto() { Id = editedArticle.Id };
    }

    public async Task DeleteArticles(List<int> articleIds)
    {
        var articlesToDelete = await dbContext.Articles.Where(x => articleIds.Contains(x.Id)).ToListAsync();
        dbContext.RemoveRange(articlesToDelete);
        await dbContext.SaveChangesAsync();
    }
}