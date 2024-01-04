using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Dto.SiteContents;

public class GridDto : BaseComponentDto
{
    public List<BaseComponentDto> Components { get; set; } = new List<BaseComponentDto>();
    public decimal GridWidth { get; set; }
}