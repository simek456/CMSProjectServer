namespace CMSProjectServer.Domain.Dto;

public class CommentDto
{
    public int Id { get; set; }
    public string Contents { get; set; }
    public string Author { get; set; }
    public int ArticleId { get; set; }
}