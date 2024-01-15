using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public interface ICommentService
{
    Task<Result<int>> AddComment(int articleId, string username, CommentDto comment);

    Task<Result<int>> EditComment(int articleId, string username, CommentDto comment);

    Task<CommentListDto> GetCommentsForArticle(int articleId);

    Task<bool> RemoveComment(int commentId, string username, bool isAdmin = false);
}