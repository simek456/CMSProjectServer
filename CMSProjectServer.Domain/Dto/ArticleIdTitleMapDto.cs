using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Dto;

public class ArticleIdTitleMapDto
{
    public List<IdTitlePairDto> Articles { get; set; } = new List<IdTitlePairDto>();
}