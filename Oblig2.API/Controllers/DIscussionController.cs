using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig2.API.Data;
using Oblig2.API.DTO;
using Oblig2.API.Models;
using Oblig2.API.Models.Data;

namespace Oblig2.API.Controllers;



[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DiscussionController : ControllerBase
{
    private readonly IAppRepository _rep;
    private readonly IMapper _mapper;
    private readonly ILogger<DiscussionController> _logger;


    
    //Attributes
    public DiscussionController(IAppRepository rep, IMapper mapper, ILogger<DiscussionController> logger)
    {
            _rep = rep;
            _mapper = mapper;
            _logger = logger;
    }


    //Methods
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult> GetDiscussions(){

        _logger.LogInformation("Acquiring to get all discussions");

        var discussions = await _rep.GetDiscussions();
        
        if(discussions == null){
            _logger.LogWarning("No discussion object found");
            return NotFound();
        }
        
        _logger.LogInformation($"Retrieved all discussions | Amount: {discussions.Count} discussions");
        return Ok(discussions);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Discussion>> GetDiscussion(int id){

        _logger.LogInformation($"Acquiring to get discussion with Id: {id}");

        var discussion = await _rep.GetDiscussionById(id);

        if(discussion == null){
            _logger.LogWarning($"No discussion object found with Id: {id}");
            return NotFound();
        }
        
        _logger.LogInformation($"Retrieved discussion with Id: {id}");
        return Ok(discussion);
    }
     

 
    [HttpPost("CreateDiscussion")]
    public async Task<ActionResult<Discussion>> CreateDiscussion(Discussion discussion){

        try{
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _logger.LogInformation($"Acquiring to create a discussion initiated by the UserId: {userId}");
            
            if(await _rep.CreateDiscussion(discussion,userId)){
                _logger.LogInformation("Discussion created successfully");
                return StatusCode(201);
            }else {
                _logger.LogWarning("Failed to create discussion");
                return BadRequest("Failed creating a discussion");
            }
        }catch(Exception e){
            _logger.LogError( "Error creating discussion: ",e);
            return StatusCode(500, "Internal server error");
        }
        
    }



    [HttpPost("{id}/CreateComment")]
    public async Task<ActionResult<Comment>> CreateComment(int id, Comment comment){

        try{
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _logger.LogInformation($"Acquiring to create a commment initiated by the UserId: {userId}");

            if(await _rep.CreateComment(id,comment,userId)){
                _logger.LogInformation("Comment created successfully");
                return StatusCode(201);
            }else {
                _logger.LogWarning("Failed to create comment");
                return BadRequest("Failed creating a comment");
            }
        }catch(Exception e){
            _logger.LogError( "Error creating comment: ",e);
            return StatusCode(500, "Internal server error");
        }
         

         
    }

    
   [HttpPut("{discussionId}")]
    public async Task<ActionResult<Discussion>> UpdateDiscussion(int discussionId, Discussion updatedDiscussion){

        try{
            _logger.LogInformation($"Acquiring to update a discussion with the Discussion Id: {discussionId}");
            if(await _rep.UpdateDiscussion(discussionId, updatedDiscussion)){
                _logger.LogInformation("Discussion updated successfully");
                return Ok(updatedDiscussion);
            }else {
                _logger.LogWarning("Failed to update a discussion");
                return BadRequest("Failed updating a discussion");
            }
        }catch(Exception e){
            _logger.LogError( "Error updating comment: ",e);
            return StatusCode(500, "Internal server error");
        }
        

    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDiscussion(int id){

        try{
            _logger.LogInformation($"Acquiring to delete a discussion with the Id: {id}");
            if(await _rep.DeleteDiscussion(id)){
                _logger.LogInformation("Discussion deleted successfully");
                return Ok();
            }else {
                _logger.LogWarning("Failed to delete a discussion");
                return BadRequest("Failed deleting a discussion");
            } 
        }catch(Exception e){
            _logger.LogError( "Error deleting comment: ",e);
            return StatusCode(500, "Internal server error");
        }
         
    }

}