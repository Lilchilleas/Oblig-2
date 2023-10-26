using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
      
        public string Content { get; set; } = default!;

        
        //Navigation Properties
        //public IdentityUser CreatedBy { get; set; }
        public Discussion Discussion { get; set; } = default!;
        public Comment ParentComment { get; set; } = default!;
        public int? ParentCommentId { get; set; } 
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();  
    }
}