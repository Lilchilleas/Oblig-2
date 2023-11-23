using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oblig2.API.Models;
using Oblig2.API.Models.Data;

namespace Oblig2.API.Data
{
    public class DbInit
    {
        
        public static async Task Seed(IApplicationBuilder app){


        using var serviceScope = app.ApplicationServices.CreateScope();
        DataContext context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        var _repository = serviceScope.ServiceProvider.GetRequiredService<IAuthRepository>();
       ;
   

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            if (!context.Users.Any())
            {

    
               var user1 = new User
                {
                    Username = "pavlovicachilleas@gmail.com"
                };
                await _repository.Register(user1, "123Hellas#");
      
                var user2 = new User
                {
                    Username = "kincetube30@gmail.com"
                };
                await _repository.Register(user2, "123Hellas#");

                var user3 = new User
                {
                    Username = "lilchilleas@gmail.com"
                };
                await _repository.Register(user3, "123Hellas#");


                user1 = context.Users.FirstOrDefault(u => u.Username == "pavlovicachilleas@gmail.com");
                user2 = context.Users.FirstOrDefault(u => u.Username == "kincetube30@gmail.com");
                user3 = context.Users.FirstOrDefault(u => u.Username == "lilchilleas@gmail.com");

                if (!context.Discussion.Any())
                {
                    var discussion = new List<Discussion>
                    {
                        new Discussion {Title = "AC MILAN VS INTER", Content = "Cardinale recently celebrated an anniversary with Milan, more specifically one year since his fund RedBird Capital Partners officially completed the acquisition of the club from Elliott Management just over a year and two weeks ago.\r\n\r\nTrying to condense Milan’s rollercoaster summer is difficult, but essentially Zlatan Ibrahimovic retired after the last game of the 2022-23 season, Paolo Maldini and Ricky Massara were sacked as directors, fan favourite Sandro Tonali was sold in a club-record deal and then 10 signings followed.\r\n\r\nIt is those signings and the improvement of the squad (on paper) which have Milan fans eager to see where this season goes. Three wins out of three is a good start to the new campaign, but the toughest test comes tomorrow against Inter.",CreatedBy = user1},
                        new Discussion {Title = "AC Milan Defender To Miss Derby Della Madonnina Clash Vs Inter Milan Through Injury", Content = "AC Milan’s starting line-up for the derby against Inter will be almost identical to the side that took to the field in the first three games of the season, but only almost.\r\n\r\nAs MilanNews writes, Stefano Pioli will have to do without both Fikayo Tomori, who is suspended for one match after the red card against Roma, and Pierre Kalulu, who got injured over the break in training at Milanello.", CreatedBy = user2}
                    };
                    context.Discussion.AddRange(discussion);
                    await context.SaveChangesAsync();

                    if (!context.Comments.Any())
                    {
                       
                        var comment1 = new Comment{Content = "Nice post",DiscussionId = 1 ,CreatedBy = user2};
                        var comment2 = new Comment{Content = "Yes it was ",DiscussionId = 1, ParentCommentId = 1, CreatedBy = user3};
                        var comment3 = new Comment{Content = "Inter will lose",DiscussionId = 1 ,CreatedBy = user1 };

                        context.Comments.AddRange(comment1);
                        context.Comments.AddRange(comment2);
                        context.Comments.AddRange(comment3);
                        await context.SaveChangesAsync();
                    }

                }
            }
        }
    }
}