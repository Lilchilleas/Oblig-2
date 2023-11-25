using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.DTO
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }
    }
}