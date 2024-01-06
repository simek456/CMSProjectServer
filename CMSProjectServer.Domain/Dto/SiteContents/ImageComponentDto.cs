using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Dto.SiteContents;

public class ImageComponentDto : BaseComponentDto
{
    public string? imgPath { get; set; }
    public string? description { get; set; }
}