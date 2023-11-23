using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Oblig2.API.Models.Data
{
    public class DataContext : DbContext
    {
        //Constructor
        public DataContext(DbContextOptions<DataContext> options): base (options) { }
        
        //Attributes
        public DbSet<Discussion> Discussion {get; set;}
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users {get; set;}
    }
}