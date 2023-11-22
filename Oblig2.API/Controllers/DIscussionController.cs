using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig2.API.Models;
using Oblig2.API.Models.Data;

namespace Oblig2.API.Controllers;



[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DiscussionController : ControllerBase
{
    private readonly DataContext _db;

    //Attributes
    public DiscussionController(DataContext db)
    {
            _db = db;
        
    }


    //Methods
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult> GetDiscussions(){

        var discussions = await _db.Discussion.ToListAsync();
        
        if(discussions == null){
            return NotFound();
        }

        return Ok(discussions);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Discussion>> GetDiscussion(int id){

       var discussion = await _db.Discussion
            .Include(x => x.CreatedBy)
            .Include(x => x.Comments)
                .ThenInclude(c => c.CreatedBy) // Include creator of each comment
            .Include(x => x.Comments)
                .ThenInclude(c => c.Replies)
                    .ThenInclude(r => r.CreatedBy) // Include creator of each reply
            .FirstOrDefaultAsync(x => x.Id == id);

        if(discussion == null){
            return NotFound();
        }

      
         
        discussion.Comments = discussion.Comments.Where(c => c.ParentCommentId == null).ToList();
        return Ok(discussion);
    }
     

 
    [HttpPost("CreateDiscussion")]
    public async Task<ActionResult<Discussion>> CreateDiscussion(Discussion discussion){
        
        if(discussion == null){
            return BadRequest("Invalid discussion object");
        }

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var user = await _db.Users.FindAsync(userId);

        if(user == null){
            return NotFound("User not found");
        }

        discussion.CreatedBy = user;

        await _db.Discussion.AddAsync(discussion);
        await _db.SaveChangesAsync();

        return StatusCode(201);
    }



    [HttpPost("{id}/CreateComment")]
    public async Task<ActionResult<Comment>> CreateComment(int id, Comment comment){
        if(comment == null){
            return BadRequest("Invalid comment object");
        }
        comment.DiscussionId = id;

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var user = await _db.Users.FindAsync(userId);

        if(user == null){
            return NotFound("User not found");
        }

        comment.CreatedBy = user;


        if(comment.ParentCommentId != null)
        {
            
            var parentComment = await _db.Comments .FindAsync(comment.ParentCommentId);

             
            comment.ParentCommentId = parentComment.Id;
        }         
        
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();

        return StatusCode(201);
    }

    
   [HttpPut("{discussionId}")]
    public async Task<ActionResult<Discussion>> UpdateDiscussion(int discussionId, Discussion updatedDiscussion){

        var discussion = await _db.Discussion.FirstOrDefaultAsync(x => x.Id == discussionId);
        if(discussion == null){
            return BadRequest("Invalid discussion object");
        }
       
        discussion.Title = updatedDiscussion.Title;
        discussion.Content = updatedDiscussion.Content;

        _db.Discussion.Update(discussion);

        await _db.SaveChangesAsync();

        return Ok(discussion);

    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDiscussion(int id){

        var discussion = await _db.Discussion.FirstOrDefaultAsync(x => x.Id == id);

         if(discussion == null){
            return BadRequest("Invalid discussion object");
        }

        _db.Discussion.Remove(discussion);
        await _db.SaveChangesAsync();
        return Ok();
    }

}