using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.DTO
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Content must have at least 1 characters")]
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }
    }
}