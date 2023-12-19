using CMSProjectServer.Domain.Enums;
using System;
using System.Collections.Generic;

namespace CMSProjectServer.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }

    public IEnumerable<Article> Articles { get; set; }
    public IEnumerable<Like> Like { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public IEnumerable<Site> EditedSites { get; set; }
}