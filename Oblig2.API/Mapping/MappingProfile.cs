using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oblig2.API.DTO;
using Oblig2.API.Models;

namespace Oblig2.API.Mapping
{
    //Manual mapping of model object to dto objects
    
    //The mapping logic is created based on the implemntation from:
    //" .NET 6 - AutoMapper & Data Transfer Objects (DTOs) from Mohamad Lawand
    //" Create Data Transfer Objects (DTOs) from Microsoft
    //" AutoMapper In .NET 6 Web API from Thiago Vivas
    public class MappingProfile
    {
        //Mapping DiscussionDTO | From a discussion object to a dto object
        public static DiscussionDto MapDiscussionDto(Discussion discussion){
            var discussionDto = new DiscussionDto {
                Id = discussion.Id,
                Title = discussion.Title,
                Content = discussion.Content,
                CreatedBy = MapUserDto(discussion.CreatedBy),
                Comments = discussion.Comments.Select(x => MapCommentDto(x)).ToList()
            };
            return discussionDto;
        }

        //Mapping CommentDTO | From a comment object to a dto object
        public static CommentDto MapCommentDto(Comment comment){
            var commentDto = new CommentDto{
                Id = comment.Id,
                Content = comment.Content,
                DiscussionId = comment.DiscussionId,
                CreatedBy = MapUserDto(comment.CreatedBy),
                ParentCommentId = comment.ParentCommentId,
                Replies = comment.Replies.Select(x => MapCommentDto(x)).ToList()
            };
            return commentDto;
        }

        //Mapping UserDTO | From a user object to a dto object
        public static UserDto MapUserDto(User user){
            var userDto = new UserDto{
                Id = user.Id,
                Username = user.Username,
            };
            return userDto;
        }

        
    }
}

// Source Reference:
// ------------------------------------------------------------------------
// - Title: .NET 6 - AutoMapper & Data Transfer Objects (DTOs)
// - Author: Mohamad Lawand
// - URL: https://dev.to/moe23/net-6-automapper-data-transfer-objects-dtos-49e
//
//
// - Title: Create Data Transfer Objects (DTOs)
// - Author: Microsoft
// - URL: https://learn.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
//
//
// - Title: AutoMapper In .NET 6 Web API
// - Author: Thiago Vivas
// - URL: https://www.c-sharpcorner.com/article/automapper-in-net-6-web-api/
 