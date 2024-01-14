using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Domain.Dto;

public class CommentDto
{
    public int Id { get; set; }
    public string Contents { get; set; }
    public string Author { get; set; }
    public int ArticleId { get; set; }
}