using JwtTokenProject.Entity;
using JwtTokenProject.Model;
using JwtTokenProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtTokenProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices Services ;
        public AuthController(IAuthServices Services)
        {
            this.Services= Services;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User?>> Register(UserDto request)
        {
           var user = await Services.RegisterAsync(request);
            if (user == null)
                return BadRequest("user already exists");
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User?>> Login(UserDto request)
        {
            var token = await Services.LoginAsync(request);
            if (token is null)
                return BadRequest("user not found");
            return Ok(token);
            
        }

     
    }
}
