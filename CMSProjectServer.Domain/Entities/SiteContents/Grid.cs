using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Entities.SiteContents;

public class Grid : BaseComponent
{
    public List<BaseComponent> Components { get; set; } = new List<BaseComponent>();
    public decimal GridWidth { get; set; }
}