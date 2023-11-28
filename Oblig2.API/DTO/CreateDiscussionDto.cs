using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.DTO
{
    public class CreateDiscussionDto
    {

        [Required]
        [MinLength(5, ErrorMessage = "Discussion must have at least 5 characters")]
        public string Title { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Content must have at least 10 characters")]
        public string Content { get; set; } 
    }
}