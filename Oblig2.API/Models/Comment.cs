using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oblig2.API.Models
{
    public class Comment
    {
        public int Id {get; set;}
        public string Content {get; set;} = default!;
        public int DiscussionId {get; set;}

    
      

        public int? ParentCommentId {get; set;}
        public Comment? ParentComment {get; set;} = default!;
       public ICollection<Comment> Replies { get; set; } = new List<Comment>();

       
    }
}