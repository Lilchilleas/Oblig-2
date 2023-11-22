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

        //Constructor
        public AppRepository(DataContext db)
        {
            _db = db;
        }


        //Methods

        public async Task<bool> UpdateDiscussion(int discussionId, Discussion updatedDiscussion){
            
            if(updatedDiscussion == null){
                return false;
            }

            var discussion = await _db.Discussion.FirstOrDefaultAsync(x => x.Id == discussionId);

            if(discussion == null){
                return false;
            }

            discussion.Title = updatedDiscussion.Title;
            discussion.Content = updatedDiscussion.Content;
            
            try{
                _db.Discussion.Update(discussion);
                await _db.SaveChangesAsync();
                return true;
            }catch(Exception e){
                return false;
            }
        }

        public async Task<bool> CreateComment(int id, Comment comment, int userId)
        {

            if (comment == null){
                return false;
            }

            var user = await _db.Users.FindAsync(userId);

            if (user == null){
                return false; 
            }

            comment.DiscussionId = id;
            comment.CreatedBy = user;

            try{
                if(comment.ParentCommentId != null)
                {
                    var parentComment = await _db.Comments .FindAsync(comment.ParentCommentId);
                    if(parentComment == null){
                        return false;
                    }
                    comment.ParentCommentId = parentComment.Id;
                }
                await _db.Comments.AddAsync(comment);
                await _db.SaveChangesAsync();
                return true;
            }catch(Exception){
                return false;
            }
        }

        public async Task<bool> CreateDiscussion(Discussion discussion, int userId)
        {

            if(discussion == null){
                return false;
            }

            var user = await _db.Users.FindAsync(userId);

            if(user == null){
                return false;
            }

            discussion.CreatedBy = user;

            try{
                await _db.Discussion.AddAsync(discussion);
                await _db.SaveChangesAsync();
                return true;
            }catch(Exception e){
                return false;
           }
        }

        public async Task<bool> DeleteDiscussion(int id)
        {
            var discussion = await _db.Discussion.FirstOrDefaultAsync(x => x.Id == id);

            if(discussion == null){
                return false;
            }

            try{
                _db.Discussion.Remove(discussion);
                await _db.SaveChangesAsync();
                return true;
            }catch(Exception e){
                return false;
            }

        }

        public async Task<Discussion?> GetDiscussionById(int id)
        {
            return await _db.Discussion
                .Include(x => x.CreatedBy)
                .Include(x => x.Comments)
                    .ThenInclude(c => c.CreatedBy)  
                .Include(x => x.Comments)
                .ThenInclude(c => c.Replies)
                    .ThenInclude(r => r.CreatedBy) 
            .FirstOrDefaultAsync(x => x.Id == id);

            
        }

        public async Task<List<Discussion>> GetDiscussions()
        {
            return await _db.Discussion.ToListAsync();
        }

        
    }
}