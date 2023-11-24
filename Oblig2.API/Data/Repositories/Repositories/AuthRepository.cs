using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oblig2.API.Models;
using Oblig2.API.Models.Data;

namespace Oblig2.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        //Attributes
        public readonly DataContext _context;

        private readonly ILogger<AuthRepository> _logger;

        //Constructor
        public AuthRepository(DataContext context, ILogger<AuthRepository> logger){
            _context = context;
            _logger = logger;
        }


        //Methods
        public async Task<User> Login(string username, string password)
        {
            _logger.LogInformation($"Database query initiated: Checking username: {username} for existence and mathcing password in database");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null){
                _logger.LogWarning("Database login operation failed: Attempted to complete with a User of null object");
                return null;
            }

            if(!VerifyPasswordHash(password,user.PasswordHash, user.PasswordSalt)){
                _logger.LogWarning("Database login operation failed: No matching passowrds in database");
                return null;
            }
            _logger.LogInformation($"Database login operation succesfull with username: {username} and verified password");
            return user;
        }

     

        public async Task<User> Register(User user, string password)
        {   
             _logger.LogInformation($"Database query initiated: Register user with username: {user.Username}");
             
            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Database register operation succesfull with username: {user.Username}");
            return user;
        }




        public async Task<bool> UserExists(string username)
        {
            _logger.LogInformation($"Database query initiated: Checking existence of username: {username} in database");
            if(await _context.Users.AnyAsync(x => x.Username == username)){
                _logger.LogInformation($"Database user exists operation succesfull with username: {username}");
                return true;
            }
            _logger.LogInformation($"Database user exist operation failed with username: {username}");
            return false;
        }

        //Helper methods


        //The CreatePasswordHash & VerifyPasswordHash logic is created based on the implemntation from:
        //"HMACSHA512 Class (System.Security.Cryptography)
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++){
                    if(computedHash[i] != passwordHash[i] ) {
                        return false;
                    }

                }
                return true;
            }
        }
    }
}



// Source Reference:
// ------------------------------------------------------------------------
// - Title: HMACSHA512 Class (System.Security.Cryptography)
// - Author: Microsoft
// - URL: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha512