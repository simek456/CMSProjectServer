using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Entities.SiteContents;

public class Row : BaseComponent
{
    public string Id { get; set; }
    public List<Grid> GridItems { get; set; } = new List<Grid>();
    public string HorizontalAligment { get; set; }
    public string VerticalAligment { get; set; }
}