namespace CMSProjectServer.Domain.Dto;

public class ArticleDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Contents { get; set; }
    public string Description { get; set; }
    public int? CategoryId { get; set; }
    public int LikeCount { get; set; }
    public bool IsLiked { get; set; }
    public string AutorName { get; set; }
}