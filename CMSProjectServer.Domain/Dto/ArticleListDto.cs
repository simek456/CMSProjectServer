using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Dto;

public class ArticleListDto
{
    public List<ArticleShortDto> Articles { get; set; } = new List<ArticleShortDto>();
}