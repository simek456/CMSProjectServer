using AutoMapper;
using CMSProjectServer.DAL;
using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using CMSProjectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

internal class ArticleService : IArticleService
{
    private readonly CMSDbContext dbContext;
    private readonly IMapper mapper;

    public ArticleService(CMSDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<Result<ArticleDto>> GetArticleById(int id)
    {
        var result = await dbContext.Articles.Include(x => x.Likes).FirstOrDefaultAsync(a => a.Id == id);
        if (result == null)
        {
            return Result<ArticleDto>.Failure();
        }
        var article = mapper.Map<ArticleDto>(result);
        article.LikeCount = result.Likes.Count;
        return article;
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

        var existingCategories = await dbContext.Categories.Where(x => articleDto.Categories.Contains(x.Category)).ToListAsync();
        foreach (var newCategory in articleDto.Categories.Where(x => existingCategories.Any(e => e.Category == x) == false))
        {
            var tag = new ArticleCategory() { Category = newCategory };
            newArticle.Categories.Add(tag);
        }
        foreach (var existingCategory in existingCategories)
        {
            newArticle.Categories.Add(existingCategory);
        }

        await dbContext.SaveChangesAsync();

        return new CreateArticleResponseDto() { Id = newArticle.Id };
    }

    public async Task<Result<CreateArticleResponseDto>> UpdateArticle(ArticleDto articleDto, string authorUsername)
    {
        var editedArticle = await dbContext.Articles.Include(x => x.Categories).Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == articleDto.Id);
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
        editedArticle.Categories.RemoveAll(x => articleDto.Categories.Contains(x.Category) == false);
        var tagsToAdd = articleDto.Categories.Where(x => editedArticle.Categories.Any(e => e.Category == x) == false).ToList();

        var existingCategories = await dbContext.Categories.Where(x => tagsToAdd.Contains(x.Category)).ToListAsync();
        foreach (var newCategory in tagsToAdd.Where(x => existingCategories.Any(e => e.Category == x) == false))
        {
            var tag = new ArticleCategory() { Category = newCategory };
            tag.Article.Add(editedArticle);
            editedArticle.Categories.Add(tag);
            dbContext.Categories.Add(tag);
        }
        foreach (var existingCategory in existingCategories)
        {
            existingCategory.Article.Add(editedArticle);
            editedArticle.Categories.Add(existingCategory);
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