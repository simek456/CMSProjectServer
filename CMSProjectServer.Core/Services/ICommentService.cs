using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;
public interface ICommentService
{
    Task<Result<bool>> AddComment(int articleId, string username, CommentDto comment);
    Task<Result<bool>> EditComment(int articleId, string username, CommentDto comment);
    Task<CommentListDto> GetCommentsForArticle(int articleId);
    Task<bool> RemoveComment(int commentId, string username, bool isAdmin = false);
}