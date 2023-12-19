using System;
using System.Collections.Generic;

namespace CMSProjectServer.Domain.Entities;

public class Article
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string Contents { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User Author { get; set; }
    public IEnumerable<ArticleTag> Tags { get; set; }
    public IEnumerable<Like> Likes { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
}