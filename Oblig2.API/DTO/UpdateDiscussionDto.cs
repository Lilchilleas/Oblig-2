using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.API.DTO
{
    public class UpdateDiscussionDto
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
    }
}