using System.Collections.Generic;

namespace CMSProjectServer.Domain.Entities;

public class ArticleTag
{
    public string Tag { get; set; }

    public IEnumerable<Article> Article { get; set; }
}