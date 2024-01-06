using CMSProjectServer.Domain.Dto.SiteContents;
using CMSProjectServer.Domain.Entities.SiteContents;
using System;

namespace CMSProjectServer.Domain.Entities;

public class OldSite
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Contents SiteContent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public User ChangeAuthor { get; set; }
}