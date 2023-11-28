using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig2.API.Data;
using Oblig2.API.DTO;
using Oblig2.API.Mapping;
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

        var discussionDto = discussions.Select(x => new DiscussionDto{
            Id = x.Id,
            Title = x.Title,
            Content = x.Content
        }).ToList();
        
        _logger.LogInformation($"Retrieved all discussions | Amount: {discussions.Count} discussions");
        return Ok(discussionDto);
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

        var discussionDto = MappingProfile.MapDiscussionDto(discussion);

        discussionDto.Comments = discussionDto.Comments.Where(x => x.ParentCommentId == null).ToList();
        
    
        _logger.LogInformation($"Retrieved discussion with Id: {id}");
        return Ok(discussionDto);
    }
     

 
    [HttpPost("CreateDiscussion")]
    public async Task<ActionResult<Discussion>> CreateDiscussion(CreateDiscussionDto createDiscussionDto){

        try{
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _logger.LogInformation($"Acquiring to create a discussion initiated by the UserId: {userId}");
            
            var discussion = new Discussion{
                Title = createDiscussionDto.Title,
                Content = createDiscussionDto.Content
            };

            if(await _rep.CreateDiscussion(discussion,userId)){
                _logger.LogInformation("Discussion created successfully");
                var discussionDto = new DiscussionDto{
                    Id = discussion.Id,
                    Title = discussion.Title,
                    Content = discussion.Content,
                };
                return StatusCode(201,discussionDto);
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
    public async Task<ActionResult<Comment>> CreateComment(int id, CreateCommentDto createCommentDto){

        try{
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _logger.LogInformation($"Acquiring to create a commment initiated by the UserId: {userId}");
            var comment = new Comment{
                Content = createCommentDto.Content,
                ParentCommentId = createCommentDto.ParentCommentId
            };
            if(await _rep.CreateComment(id,comment,userId)){
                _logger.LogInformation("Comment created successfully");
                var commentDto = new CommentDto{
                    Id = comment.Id,
                    Content =comment.Content,
                    ParentCommentId = comment.ParentCommentId,
                };
                return StatusCode(201,commentDto);
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
    public async Task<ActionResult<Discussion>> UpdateDiscussion(int discussionId, UpdateDiscussionDto updateDiscussionDto){

        try{
            _logger.LogInformation($"Acquiring to update a discussion with the Discussion Id: {discussionId}");
            var updatedDiscussion = new Discussion{
                Id = discussionId,
                Title = updateDiscussionDto.Title,
                Content = updateDiscussionDto.Content
            };
            if(await _rep.UpdateDiscussion(discussionId, updatedDiscussion)){
                _logger.LogInformation("Discussion updated successfully");
                var updadeDiscussionDto = new DiscussionDto
                {
                    Id = updatedDiscussion.Id,
                    Title = updatedDiscussion.Title,
                    Content = updatedDiscussion.Content
                };
                return Ok(updateDiscussionDto);
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