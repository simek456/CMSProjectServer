using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CMSProjectServer.Domain.Entities;

public class User : IdentityUser
{
    public string? Name { get; set; }
    public string? About { get; set; }
    public DateTime CreatedAt { get; set; }

    public IEnumerable<Article> Articles { get; set; }
    public IEnumerable<Like> Like { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public IEnumerable<Site> EditedSites { get; set; }
}