using System;

namespace CMSProjectServer.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int ArticleId { get; set; }
    public string Contents { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User User { get; set; }
    public Article Article { get; set; }
}