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

    public async Task<Result<ArticleDto>> GetArticleById(int id, string? username)
    {
        var result = await dbContext.Articles.Include(x => x.Likes).Include(x => x.Category).Select(x => new { Author = x.Author.UserName, Article = x }).FirstOrDefaultAsync(a => a.Article.Id == id);
        if (result == null)
        {
            return Result<ArticleDto>.Failure();
        }
        bool isLiked = false;
        if (username != null)
        {
            isLiked = dbContext.Likes.Any(x => x.User.UserName == username);
        }
        var article = mapper.Map<ArticleDto>(result.Article);
        article.LikeCount = result.Article.Likes.Count;
        article.IsLiked = isLiked;
        article.AutorName = result.Author;
        return article;
    }

    public async Task<Result<ArticleShortDto>> GetArticleShortById(int id)
    {
        var result = await dbContext.Articles.Include(x => x.Author).Include(x => x.Category).Select(x => new { LikeCount = x.Likes.Count, Article = x }).FirstOrDefaultAsync(a => a.Article.Id == id);
        if (result == null)
        {
            return Result<ArticleShortDto>.Failure();
        }
        var article = mapper.Map<ArticleShortDto>(result.Article);
        article.AuthorName = result.Article.Author.UserName;
        article.LikeCount = result.LikeCount;
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
        if (category == null && articleDto.CategoryId != null)
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
        if (category == null && articleDto.CategoryId != null)
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

    public async Task<ArticleIdTitleMapDto> GetArticleIdNameMap(int pageSize, int? page, int? categoryId, string? order, string? authorId, string? title)
    {
        var articleList = await GetArticleListQuery(pageSize, page, categoryId, order, authorId, title, out var totalCount)
            .Select(x => new IdTitlePairDto() { Id = x.Id, Title = x.Title })
            .ToListAsync();
        var result = new ArticleIdTitleMapDto();
        result.Articles = articleList;
        result.TotalArticlecount = totalCount;
        return result;
    }

    public async Task<ArticleListDto> GetArticleListShort(int pageSize, int? page, int? categoryId, string? order, string? authorId, string? title)
    {
        var articleList = await GetArticleListQuery(pageSize, page, categoryId, order, authorId, title, out var totalCount)
            .Select(x => new { Article = x, LikeCount = x.Likes.Count })
            .ToListAsync();
        var result = new ArticleListDto()
        {
            Articles = articleList.Select(x =>
            {
                var dto = mapper.Map<ArticleShortDto>(x.Article);
                dto.AuthorName = x.Article.Author.UserName;
                dto.LikeCount = x.LikeCount;
                return dto;
            }).ToList(),
            TotalArticleCount = totalCount
        };
        return result;
    }

    private IQueryable<Article> GetArticleListQuery(int pageSize, int? page, int? categoryId, string? order, string? authorId, string? title, out int totalCount)
    {
        IQueryable<Article> query = dbContext.Articles.Include(x => x.Author).Include(x => x.Category);

        if (categoryId != null)
        {
            query = query.Where(x => x.Category.Id == categoryId);
        }
        if (authorId != null)
        {
            query = query.Where(x => x.Author.Id == authorId);
        }
        if (title != null)
        {
            query = query.Where(x => EF.Functions.ILike(x.Title, $"%{title}%"));
        }
        totalCount = query.Count();
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