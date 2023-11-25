using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.DTO
{
    public class CommentDto
    {
         public int Id {get; set;}
        public string Content {get; set;} = default!;



        public int DiscussionId {get; set;}
        public UserDto? CreatedBy { get; set; }
        public int? ParentCommentId {get; set;}
        public CommentDto? ParentComment {get; set;} = default!;
        public ICollection<CommentDto> Replies { get; set; } = new List<CommentDto>();
    }
}