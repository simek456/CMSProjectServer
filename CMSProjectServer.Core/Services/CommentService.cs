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

public class CommentService : ICommentService
{
    private readonly CMSDbContext dbContext;

    public CommentService(CMSDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<CommentListDto> GetCommentsForArticle(int articleId)
    {
        var comments = await dbContext.Comments
            .Where(x => x.Article.Id == articleId)
            .Select(x => new CommentDto { ArticleId = x.Article.Id, Author = x.Author.UserName, Id = x.Id, Contents = x.Contents })
            .ToListAsync();
        return new CommentListDto() { CommentList = comments };
    }

    public async Task<Result<bool>> AddComment(int articleId, string username, CommentDto comment)
    {
        var article = await dbContext.Articles.FirstOrDefaultAsync(x => x.Id == articleId);
        if (article == null)
        {
            return Result<bool>.Failure("Article doesnt exist");
        }
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        if (user == null)
        {
            return Result<bool>.Failure("User doesnt exist");
        }
        var commentEntity = new Comment()
        {
            CreatedAt = DateTime.UtcNow,
            Article = article,
            Author = user,
            Contents = comment.Contents,
        };
        dbContext.Add(commentEntity);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Result<bool>> EditComment(int articleId, string username, CommentDto comment)
    {
        var commentEntity = await dbContext.Comments.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == comment.Id);
        if (commentEntity is null)
        {
            return Result<bool>.Failure("comment doesnt exist");
        }
        if (commentEntity.Author.UserName != username)
        {
            return Result<bool>.Failure("You are not the author!");
        }
        commentEntity.UpdatedAt = DateTime.UtcNow;
        commentEntity.Contents = comment.Contents;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveComment(int commentId, string username, bool isAdmin = false)
    {
        var comment = await dbContext.Comments.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == commentId);
        if (comment is not null
            && (isAdmin || comment.Author.UserName == username))
        {
            dbContext.Remove(comment);
            await dbContext.SaveChangesAsync();

            return true;
        }
        return false;
    }
}