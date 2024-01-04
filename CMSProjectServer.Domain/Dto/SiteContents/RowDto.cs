using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Dto.SiteContents;

public class RowDto : BaseComponentDto
{
    public string Id { get; set; }
    public List<GridDto> GridItems { get; set; } = new List<GridDto>();
    public string HorizontalAligment { get; set; }
    public string VerticalAligment { get; set; }
}