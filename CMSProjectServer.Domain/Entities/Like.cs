namespace CMSProjectServer.Domain.Entities;

public class Like
{
    public int UserId { get; set; }
    public int ArticleId { get; set; }

    public User User { get; set; }
    public Article Article { get; set; }
}