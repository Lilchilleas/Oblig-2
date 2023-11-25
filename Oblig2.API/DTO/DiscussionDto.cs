using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.DTO
{
    public class DiscussionDto
    {
        public int Id { get; set; } 
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
      

        public UserDto? CreatedBy { get; set; }
         public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}