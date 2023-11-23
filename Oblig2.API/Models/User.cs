using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.Models
{
    public class User
    {
        [Key]
        public int Id {get; set;}

         
        public string Username {get; set;} = default!;
        public byte[] PasswordHash {get; set;} = default!;
        public byte[] PasswordSalt {get; set;} = default!;
    }
}