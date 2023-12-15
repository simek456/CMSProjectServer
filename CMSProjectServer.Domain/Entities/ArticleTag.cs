namespace CMSProjectServer.Domain.Entities;

public class ArticleTag
{
    public string Tag { get; set; }
    public int ArticleId { get; set; }

    public Article Article { get; set; }
}