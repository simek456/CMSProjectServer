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
        var result = await dbContext.Articles.Include(x => x.Likes).Include(x => x.Category).FirstOrDefaultAsync(a => a.Id == id);
        if (result == null)
        {
            return Result<ArticleDto>.Failure();
        }
        var article = mapper.Map<ArticleDto>(result);
        article.LikeCount = result.Likes.Count;
        return article;
    }

    public async Task<Result<ArticleShortDto>> GetArticleShortById(int id)
    {
        var result = await dbContext.Articles.Include(x => x.Author).Include(x => x.Category).FirstOrDefaultAsync(a => a.Id == id);
        if (result == null)
        {
            return Result<ArticleShortDto>.Failure();
        }
        var article = mapper.Map<ArticleShortDto>(result);
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
        var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == articleDto.CategoryId);
        if (category == null)
        {
            return Result<CreateArticleResponseDto>.Failure("Category doesn't exist");
        }
        var newArticle = new Article();
        newArticle.Title = articleDto.Title;
        newArticle.Contents = articleDto.Contents;
        newArticle.Description = articleDto.Description;
        newArticle.CreatedAt = DateTime.UtcNow;
        newArticle.Author = author;
        newArticle.Category = category;

        dbContext.Add(newArticle);

        await dbContext.SaveChangesAsync();

        return new CreateArticleResponseDto() { Id = newArticle.Id };
    }

    public async Task<Result<CreateArticleResponseDto>> UpdateArticle(ArticleDto articleDto, string authorUsername)
    {
        var editedArticle = await dbContext.Articles.Include(x => x.Category).Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == articleDto.Id);
        if (editedArticle == null)
        {
            return Result<CreateArticleResponseDto>.Failure("Article doen't exist");
        }
        if (editedArticle.Author.UserName != authorUsername)
        {
            return Result<CreateArticleResponseDto>.Failure("You are not the author");
        }
        var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == articleDto.CategoryId);
        if (category == null)
        {
            return Result<CreateArticleResponseDto>.Failure("Category doesn't exist");
        }

        editedArticle.Title = articleDto.Title;
        editedArticle.Contents = articleDto.Contents;
        editedArticle.Description = articleDto.Description;
        editedArticle.UpdatedAt = DateTime.UtcNow;
        editedArticle.Category = category;

        await dbContext.SaveChangesAsync();

        return new CreateArticleResponseDto() { Id = editedArticle.Id };
    }

    public async Task DeleteArticles(List<int> articleIds)
    {
        var articlesToDelete = await dbContext.Articles.Where(x => articleIds.Contains(x.Id)).ToListAsync();
        dbContext.RemoveRange(articlesToDelete);
        await dbContext.SaveChangesAsync();
    }

    public async Task<ArticleIdNameMapDto> GetArticleIdNameMap(int pageSize, int? page, int? categoryId, string? order, string? authorId)
    {
        var articleList = await GetArticleListQuery(pageSize, page, categoryId, order, authorId)
            .Select(x => new { Id = x.Id, Name = x.Title })
            .ToListAsync();
        var result = new ArticleIdNameMapDto();
        foreach (var article in articleList)
        {
            result.Articles.Add((article.Id, article.Name));
        }
        return result;
    }

    public async Task<ArticleListDto> GetArticleListShort(int pageSize, int? page, int? categoryId, string? order, string? authorId)
    {
        var articleList = await GetArticleListQuery(pageSize, page, categoryId, order, authorId)
            .ToListAsync();
        var result = new ArticleListDto()
        {
            Articles = articleList.Select(x => mapper.Map<ArticleShortDto>(x)).ToList(),
        };
        return result;
    }

    private IQueryable<Article> GetArticleListQuery(int pageSize, int? page, int? categoryId, string? order, string? authorId)
    {
        IQueryable<Article> query = dbContext.Articles;

        if (categoryId != null)
        {
            query = query.Where(x => x.Category.Id == categoryId);
        }
        if (authorId != null)
        {
            query = query.Where(x => x.Author.Id == authorId);
        }
        if (page != null)
        {
            query = query.Skip(pageSize * page.Value);
        }
        query = query.Take(pageSize);
        switch (order)
        {
            case SortingType.NameDescending:
                query = query.OrderByDescending(x => x.Title);
                break;

            case SortingType.NameAscending:
                query = query.OrderBy(x => x.Title);
                break;

            case SortingType.TimeDescending:
                query = query.OrderByDescending(x => x.CreatedAt);
                break;

            case SortingType.TimeAscending:
                query = query.OrderBy(x => x.CreatedAt);
                break;

            case SortingType.LikeDescending:
                query = query.OrderByDescending(x => x.Likes.Count);
                break;

            case SortingType.LikeAscending:
                query = query.OrderBy(x => x.Likes.Count);
                break;

            default: break;
        }
        return query;
    }
}