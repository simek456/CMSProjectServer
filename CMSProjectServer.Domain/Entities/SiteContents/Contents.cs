using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Entities.SiteContents;

public class Contents
{
    public List<MenuItem> MenuItems { get; set; }
    public Footer Footer { get; set; }
    public Header Header { get; set; }
}