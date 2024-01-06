using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public interface ILikeService
{
    Task<bool> DislikeLike(string username, int articleId);

    Task<bool> Like(string username, int articleId);
}