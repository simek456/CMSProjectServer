using CMSProjectServer.Domain.Entities.SiteContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Dto.SiteContents;

public class ContentsDto
{
    public List<MenuItemDto> MenuItems { get; set; }
    public FooterDto Footer { get; set; }
    public HeaderDto Header { get; set; }
}