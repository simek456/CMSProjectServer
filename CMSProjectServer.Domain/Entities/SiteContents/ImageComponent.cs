using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Entities.SiteContents;

public class ImageComponent : BaseComponent
{
    public string? imgPath { get; set; }
    public string? description { get; set; }
}