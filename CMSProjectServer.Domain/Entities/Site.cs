using System;
using System.Text.Json.Nodes;

namespace CMSProjectServer.Domain.Entities;

public class Site
{
    public int Id { get; set; }
    public string Name { get; set; }
    public JsonObject SiteContent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User ChangeAuthor { get; set; }
}