using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DemoApp.API.Dtos;
using DemoApp.API.Models;
using DemoApp.API.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DemoApp.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository _repo;
        public readonly IConfiguration _configuration ;
        public AuthController (IAuthRepository repo, IConfiguration configuration) {
            _configuration = configuration;
            _repo = repo;

        }

        [HttpPost ("Register")]
        public async Task<IActionResult> Register (RegisterUserDto registerUserDto) {
            var name = registerUserDto.UserName.ToLower ();
            if (await _repo.UserExists (name))
                return BadRequest ("user alreday exists");

            var userToBeCreated = new User { UserName = name };
            await _repo.Register (userToBeCreated, registerUserDto.Password);
            return StatusCode (201);
        }

        [HttpPost ("Login")]
        public async Task<IActionResult> Login (LoginUserDto loginUserDto) {
            var userToBeLogged = await _repo.Login (loginUserDto.UserName, loginUserDto.Password);
            if (userToBeLogged is null)
                return Unauthorized ();
            var claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, userToBeLogged.Id.ToString ()),
                new Claim (ClaimTypes.Name, userToBeLogged.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject= new ClaimsIdentity( claims),
                Expires= DateTime.Now.AddDays(1),
                SigningCredentials=creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token=tokenHandler.WriteToken(token)
                });


        }

    }
}