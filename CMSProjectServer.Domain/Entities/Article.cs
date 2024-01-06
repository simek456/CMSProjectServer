using System;
using System.Collections.Generic;

namespace CMSProjectServer.Domain.Entities;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Contents { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User Author { get; set; }
    public ArticleCategory Category { get; set; }
    public List<Like> Likes { get; set; } = new List<Like>();
    public List<Comment> Comments { get; set; } = new List<Comment>();
}