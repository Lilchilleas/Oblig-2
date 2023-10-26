using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.DTO
{
    public class UserRegistrationDto
    {
        [Required]
        public string Username {get; set;}

        [Required]
        [StringLength(15, MinimumLength = 8,ErrorMessage ="Minimum 8 and maximum 15 length")]
        public string Password {get; set;}
    }
}