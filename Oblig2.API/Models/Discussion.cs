using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.Models
{
    public class Discussion
    {
        [Key]
        public int Id { get; set; } 

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;
        

        //Navigation Properties
        //public IdentityUser CreatedBy { get; set; }
        public string CreatedById { get; set; }  = default!;
        //public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}