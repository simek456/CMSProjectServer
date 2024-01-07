using System;
using System.Collections.Generic;

namespace CMSProjectServer.Domain.Dto;

public class ArticleShortDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CategoryId { get; set; }
    public Guid AuthorId { get; set; }
}