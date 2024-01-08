using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Dto;

public class ArticleIdNameMapDto
{
    public List<(int, string)> Articles { get; set; } = new List<(int, string)>();
}