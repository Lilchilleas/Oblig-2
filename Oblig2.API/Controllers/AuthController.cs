
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oblig2.API.Data;
using Oblig2.API.DTO;
using Oblig2.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Oblig2.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    
        //Attributes
        private readonly IAuthRepository _repo;

        private readonly IConfiguration _configuration;

        private readonly ILogger<AuthController> _logger;


        //Controller
        public AuthController(IAuthRepository repo, IConfiguration configuraton, ILogger<AuthController> logger){
            _repo = repo;
            _configuration = configuraton;
            _logger = logger;
        }


        //Methods
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto userToRegister){

            try{
                userToRegister.Username = userToRegister.Username.ToLower();    
                _logger.LogInformation($"Registration attempt to create a user with the Username: {userToRegister.Username}");
                
                if(await _repo.UserExists(userToRegister.Username)){
                    _logger.LogWarning($"Registration failed: Username {userToRegister.Username} already exists");
                    return BadRequest("Username exists");
                }

                var user = new User{
                    Username = userToRegister.Username
                };

                var createdUser = await _repo.Register(user,userToRegister.Password);

                _logger.LogInformation($"User: {userToRegister.Username} registered successfully");
                return StatusCode(201);
            }catch(Exception e){
                _logger.LogError( "Error creating a user: ",e);
                return StatusCode(500, "Internal server error");
            }
             
        }

        

        //The Login logic with Token generation is created based on the implemntation from:
        //"Build an app with ASPNET Core and Angular from scratch" by Neil Cummings.
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userToLogin){

            try{
                _logger.LogInformation($"Login attempt with Username: {userToLogin.Username.ToLower()}");

                var user = await _repo.Login(userToLogin.Username.ToLower(),userToLogin.Password);
                 
                if(user == null){
                    _logger.LogWarning($"Login failed: Username: {userToLogin.Username}");
                    return Unauthorized();
                } 

                var claims = new []
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username) 
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);


                var tokenDescriptor = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                _logger.LogInformation($"User: {userToLogin.Username} logged in successfully");

                return Ok(new {
                    token = tokenHandler.WriteToken(token)
                });
            }catch(Exception e){
                _logger.LogError( "Error occur during login: ",e);
                return StatusCode(500, "Internal server error");
            }

             
        }
    }
}


// Source Reference:
// ------------------------------------------------------------------------
// - [Title: Build an app with ASPNET Core and Angular from scratch ]
// - Author: [Neil Cummings]
// - URL: [https://www.udemy.com/course/build-an-app-with-aspnet-core-and-angular-from-scratch/#instructor-1]

