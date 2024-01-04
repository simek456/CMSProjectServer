using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Entities.SiteContents;

public class MenuItem
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<Row> RowItems { get; set; }
}