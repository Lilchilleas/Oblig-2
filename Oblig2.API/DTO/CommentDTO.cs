using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.DTO
{
  public class CommentDto
{
    public int Id {get; set;}
    public string Content {get; set;}
    public int DiscussionId {get; set;}
    public int? ParentCommentId {get; set;}
    public List<CommentDto> Replies { get; set; } = new List<CommentDto>();
}
}