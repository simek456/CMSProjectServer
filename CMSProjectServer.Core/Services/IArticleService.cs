using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public interface IArticleService
{
    Task<Result<CreateArticleResponseDto>> CreateArticle(ArticleDto articleDto, string authorUsername);

    Task DeleteArticles(List<int> articleIds);

    Task<Result<ArticleDto>> GetArticleById(int id, string? username);

    Task<ArticleIdTitleMapDto> GetArticleIdNameMap(int pageSize, int? page, int? categoryId, string? order, string? authorId);

    Task<ArticleListDto> GetArticleListShort(int pageSize, int? page, int? categoryId, string? order, string? authorId);

    Task<Result<ArticleShortDto>> GetArticleShortById(int id);

    Task<Result<CreateArticleResponseDto>> UpdateArticle(ArticleDto articleDto, string authorUsername);
}