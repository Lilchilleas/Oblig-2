using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oblig2.API.Models;

namespace Oblig2.API.Data
{
    public interface IAppRepository
    {
        //Get
        Task<List<Discussion>> GetDiscussions();
        Task<Discussion?> GetDiscussionById(int id);

        //Create
        Task<bool> CreateDiscussion(Discussion discussion, int userId);
        Task<bool> CreateComment(int id, Comment comment, int userId);

        //Update
        Task<bool> UpdateDiscussion(int discussionId, Discussion updatedDiscussion);

        //Delete
        Task<bool> DeleteDiscussion(int id);


    }
}