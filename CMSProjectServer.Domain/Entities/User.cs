using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CMSProjectServer.Domain.Entities;

public class User : IdentityUser
{
    public string? Name { get; set; }
    public string? About { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<Article> Articles { get; set; } = new List<Article>();
    public List<Like> Like { get; set; } = new List<Like>();
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Site> EditedSites { get; set; } = new List<Site>();
}