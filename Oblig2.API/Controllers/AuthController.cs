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


        //Controller
        public AuthController(IAuthRepository repo, IConfiguration configuraton){
            _repo = repo;
            _configuration = configuraton;
        }


        //Methods
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto userToRegister){
            userToRegister.Username = userToRegister.Username.ToLower();
            if(await _repo.UserExists(userToRegister.Username)){
                return BadRequest("Username exists");
            }

            var user = new User{
                Username = userToRegister.Username
            };

            var createdUser = await _repo.Register(user,userToRegister.Password);

            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userToLogin){
            var user = await _repo.Login(userToLogin.Username.ToLower(),userToLogin.Password);

            if(user == null){
                return Unauthorized();
            } 

            //Source /////DONT FORGET TO SOURCE THIS |TOKEN| AND EXPLAIN THE CODE
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

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });


        }














    }
}