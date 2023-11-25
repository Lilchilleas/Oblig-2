using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.DTO
{
    public class UserDto
    {
        public int Id {get; set;}
        public string Username {get; set;} = default!;

    }
}