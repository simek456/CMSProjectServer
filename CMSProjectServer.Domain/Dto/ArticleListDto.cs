using System.Collections.Generic;

namespace CMSProjectServer.Domain.Dto;

public class ArticleListDto
{
    public List<ArticleShortDto> Articles { get; set; } = new List<ArticleShortDto>();
    public int TotalArticleCount { get; set; }
}