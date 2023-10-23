using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig2.API.Models.Data;

namespace Oblig2.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscussionController : ControllerBase
{
    private readonly DataContext _context;

    //Attributes
    public DiscussionController(DataContext context)
    {
            _context = context;
        
    }


    //Methods
    [HttpGet("getDiscussions")]
    public async Task<ActionResult> getDiscussions(){
        var discussions = await _context.Discussion.ToListAsync();
        return Ok(discussions);
    }


}