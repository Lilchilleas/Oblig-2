using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig2.API.Data;
using Oblig2.API.Models;
using Oblig2.API.Models.Data;

namespace Oblig2.API.Controllers;



[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DiscussionController : ControllerBase
{
    private readonly IAppRepository _rep;

    //Attributes
    public DiscussionController(IAppRepository rep)
    {
            _rep = rep;
    }


    //Methods
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult> GetDiscussions(){

        var discussions = await _rep.GetDiscussions();
        
        if(discussions == null){
            return NotFound();
        }

        return Ok(discussions);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Discussion>> GetDiscussion(int id){

        var discussion = await _rep.GetDiscussionById(id);
        if(discussion == null){
            return NotFound();
        }

      
         
        discussion.Comments = discussion.Comments.Where(c => c.ParentCommentId == null).ToList();
        return Ok(discussion);
    }
     

 
    [HttpPost("CreateDiscussion")]
    public async Task<ActionResult<Discussion>> CreateDiscussion(Discussion discussion){
    
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
       
        if(await _rep.CreateDiscussion(discussion,userId)){
            return StatusCode(201);
        }else {
            return BadRequest("Failed creating a discussion");
        }
    }



    [HttpPost("{id}/CreateComment")]
    public async Task<ActionResult<Comment>> CreateComment(int id, Comment comment){
        
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        if(await _rep.CreateComment(id,comment,userId)){
            return StatusCode(201);
        }else {
            return BadRequest("Failed creating a comment");
        }
    }

    
   [HttpPut("{discussionId}")]
    public async Task<ActionResult<Discussion>> UpdateDiscussion(int discussionId, Discussion updatedDiscussion){

       if(await _rep.UpdateDiscussion(discussionId, updatedDiscussion)){
            return Ok(updatedDiscussion);
       }else {
            return BadRequest("Failed updating a discussion");
       }

    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDiscussion(int id){

        if(await _rep.DeleteDiscussion(id)){
            return Ok();
       }else {
            return BadRequest("Failed deleting a discussion");
       } 
    }

}