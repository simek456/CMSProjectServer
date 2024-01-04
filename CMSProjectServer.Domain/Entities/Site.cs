using System;

namespace CMSProjectServer.Domain.Entities;

public class Site
{
    public int Id { get; set; }
    public string SiteContent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User ChangeAuthor { get; set; }
}