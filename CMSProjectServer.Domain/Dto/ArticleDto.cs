using System.Collections.Generic;

namespace CMSProjectServer.Domain.Dto;

public class ArticleDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Contents { get; set; }
    public string Description { get; set; }
    public int Category { get; set; }
    public int LikeCount { get; set; }
}