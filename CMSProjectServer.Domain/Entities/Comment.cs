using System;

namespace CMSProjectServer.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Contents { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User Author { get; set; }
    public Article Article { get; set; }
}