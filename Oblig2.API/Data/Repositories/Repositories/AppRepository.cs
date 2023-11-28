using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oblig2.API.Models;
using Oblig2.API.Models.Data;

namespace Oblig2.API.Data
{
    public class AppRepository : IAppRepository
    {

        //Attributes
        private readonly DataContext _db;

        private readonly ILogger<AppRepository> _logger;

        //Constructor
        public AppRepository(DataContext db, ILogger<AppRepository> logger)
        {
            _db = db;
            _logger = logger;
        }


        //Methods

        public async Task<bool> UpdateDiscussion(int discussionId, Discussion updatedDiscussion){
            try{
                _logger.LogInformation($"Database query initiated: Updating discussion with discussion ID: {discussionId}");
                if(updatedDiscussion == null){
                    _logger.LogWarning("Database update operation failed: Attempted to update discussion with null object");
                    return false;
                }

                var discussion = await _db.Discussion.FirstOrDefaultAsync(x => x.Id == discussionId);

                if(discussion == null){
                    _logger.LogWarning($"Database update operation failed: No discussion object found with Id: {discussionId}");
                    return false;
                }

                discussion.Title = updatedDiscussion.Title;
                discussion.Content = updatedDiscussion.Content;
            
             
                _db.Discussion.Update(discussion);
                await _db.SaveChangesAsync();
                _logger.LogInformation($"Database update operation succesfull with Id: {discussionId}");
                return true;
            }catch(Exception e){
                _logger.LogError("Database update operation failed for discussion ",e);
                return false;
            }
        }

        public async Task<bool> CreateComment(int id, Comment comment, int userId)
        {
            try{
                _logger.LogInformation($"Database query initiated: Creating comment with discussion ID: {id}");

                if (comment == null){
                    _logger.LogWarning("Database create operation failed: Attempted to create comment with null object");
                    return false;
                }

                var user = await _db.Users.FindAsync(userId);

                if (user == null){
                    _logger.LogWarning("Database create operation failed: Attempted to create comment with a User null object");
                    return false; 
                }

                comment.DiscussionId = id;
                comment.CreatedBy = user;

            
                if(comment.ParentCommentId != null)
                {
                    var parentComment = await _db.Comments.FindAsync(comment.ParentCommentId);

                    if(parentComment == null){
                        _logger.LogInformation($"HERE");
                        _logger.LogWarning($"Database create operation failed: No parent commend found with Id: {comment.ParentCommentId}");
                        return false;
                    }
                    comment.ParentCommentId = parentComment.Id;
                }
                await _db.Comments.AddAsync(comment);
                await _db.SaveChangesAsync();
                _logger.LogInformation($"Database creation of comment operation succesfull for Discussion Id: {id} by User {userId}");
                return true;
            }catch(Exception e){
                _logger.LogError("Database creation operation failed for comment ",e);
                return false;
            }
        }

        public async Task<bool> CreateDiscussion(Discussion discussion, int userId)
        {
            try{
                _logger.LogInformation($"Database query initiated: Creating discussion with ID: {discussion.Id}");

                if(discussion == null){
                    _logger.LogWarning("Database create operation failed: Attempted to create discussion with null object");
                    return false;
                }

                var user = await _db.Users.FindAsync(userId);

                if(user == null){
                    _logger.LogWarning("Database create operation failed: Attempted to create discussion with a User null object");
                    return false;
                }

                discussion.CreatedBy = user;

             
                await _db.Discussion.AddAsync(discussion);
                await _db.SaveChangesAsync();
                _logger.LogInformation($"Database creation operation succesfull with Id: {discussion.Id} and User: {user.Username}");
                return true;
            }catch(Exception e){
                _logger.LogError("Database creation operation failed for discussion ",e);
                return false;
           }
        }

        public async Task<bool> DeleteDiscussion(int id)
        {
            try{

                _logger.LogInformation($"Database query initiated: Deleting discussion with ID: {id}");
                var discussion = await _db.Discussion.FirstOrDefaultAsync(x => x.Id == id);

                if(discussion == null){
                    _logger.LogWarning("Database delete operation failed: Attempted to create discussion with null object");
                    return false;
                }

                _db.Discussion.Remove(discussion);
                await _db.SaveChangesAsync();
                _logger.LogInformation($"Database delete operation succesfull with Id: {discussion.Id}");
                return true;
            }catch(Exception e){
                _logger.LogError("Database delete operation failed for discussion ",e);
                return false;
            }

        }

        public async Task<Discussion?> GetDiscussionById(int id)
        {

            try{
                _logger.LogInformation($"Database query initiated: Fetching discussion with ID: {id}");

                var discussion = await _db.Discussion
                    .Include(x => x.CreatedBy)
                    .Include(x => x.Comments)
                        .ThenInclude(c => c.CreatedBy)  
                    .Include(x => x.Comments)
                    .ThenInclude(c => c.Replies)
                        .ThenInclude(r => r.CreatedBy) 
                .FirstOrDefaultAsync(x => x.Id == id);

 
                if(discussion == null){
                    _logger.LogWarning("Database get operation failed: Attempted to get single discussion with null object");
                    return null;
                }
                discussion.Comments = discussion.Comments.Where(c => c.ParentCommentId == null).ToList();
                _logger.LogInformation($"Database get operation succesfull with Id: {id}");
                return discussion;
            }catch(Exception e){
                _logger.LogError("Database get discussion operation failed ",e);
                throw;
            }
            

           

        }

        public async Task<List<Discussion>> GetDiscussions()
        {
            try{
                _logger.LogInformation($"Database query initiated: Fetching all discussions");
                
                var discussions =  await _db.Discussion.ToListAsync();

                if(!discussions.Any()){
                    _logger.LogWarning("Database get operation failed: Returned no results of discussion list");
                }else {
                    _logger.LogInformation($"Database get operation succesfull");
                }
                return discussions;
            }catch(Exception e){
                _logger.LogError("Database get discussions operation failed ",e);
                throw;
            }
        }     
    }
}