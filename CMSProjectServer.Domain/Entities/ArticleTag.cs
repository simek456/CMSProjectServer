using System.Collections.Generic;

namespace CMSProjectServer.Domain.Entities;

public class ArticleTag
{
    public string Tag { get; set; }

    public List<Article> Article { get; set; } = new List<Article>();
}