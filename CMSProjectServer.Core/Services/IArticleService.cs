using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public interface IArticleService
{
    Task<Result<CreateArticleResponseDto>> CreateArticle(ArticleDto articleDto, string authorUsername);

    Task DeleteArticles(List<int> articleIds);

    Task<Result<ArticleDto>> GetArticleById(int id);

    Task<Result<CreateArticleResponseDto>> UpdateArticle(ArticleDto articleDto, string authorUsername);
}