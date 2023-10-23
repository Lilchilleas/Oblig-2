using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.Models
{
    public class Discussion
    {
        public int id { get; set; }
        public string  title  { get; set; } = default!;
        public string content { get; set; } = default!;
    }
}