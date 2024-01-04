using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Dto.SiteContents;

public class BlockComponentDto
{
    public string Text { get; set; }
    public decimal FontSize { get; set; }
    public string FontFamili { get; set; }
    public string FontColor { get; set; }
    public string TextAlign { get; set; }
    public string VerticalAlign { get; set; }
}