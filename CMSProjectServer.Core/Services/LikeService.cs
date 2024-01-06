using CMSProjectServer.DAL;
using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public class LikeService : ILikeService
{
    private readonly CMSDbContext dbContext;

    public LikeService(CMSDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> Like(string username, int articleId)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        if (user == null)
        {
            return false;
        }
        var article = await dbContext.Articles.FirstOrDefaultAsync(x => x.Id == articleId);
        if (article == null)
        {
            return false;
        }
        var like = new Like() { Article = article, User = user };
        dbContext.Likes.Add(like);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DislikeLike(string username, int articleId)
    {
        var user = await dbContext.Users.Include(x => x.Likes).ThenInclude(x => x.Article).FirstOrDefaultAsync(x => x.UserName == username);
        if (user == null)
        {
            return false;
        }
        var like = user.Likes.FirstOrDefault(x => x.Article.Id == articleId);
        if (like == null)
        {
            return true;
        }
        user.Likes.Remove(like);
        await dbContext.SaveChangesAsync();
        return true;
    }
}