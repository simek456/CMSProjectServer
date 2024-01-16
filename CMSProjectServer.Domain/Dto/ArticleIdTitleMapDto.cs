using System.Collections.Generic;

namespace CMSProjectServer.Domain.Dto;

public class ArticleIdTitleMapDto
{
    public List<IdTitlePairDto> Articles { get; set; } = new List<IdTitlePairDto>();
    public int TotalArticlecount { get; set; }
}