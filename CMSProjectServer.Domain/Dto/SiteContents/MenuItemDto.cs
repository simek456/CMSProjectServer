using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Dto.SiteContents;

public class MenuItemDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<RowDto> RowItems { get; set; }
}