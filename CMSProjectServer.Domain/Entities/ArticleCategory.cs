using System.Collections.Generic;

namespace CMSProjectServer.Domain.Entities;

public class ArticleCategory
{
    public int Id { get; set; }
    public string Category { get; set; }

    public List<Article> Articles { get; set; } = new List<Article>();
}