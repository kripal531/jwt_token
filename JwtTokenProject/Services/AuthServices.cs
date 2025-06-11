using JwtTokenProject.data;
using JwtTokenProject.Entity;
using JwtTokenProject.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenProject.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration configuration;
        private readonly MyDbContext context;

        public AuthServices(IConfiguration configuration, MyDbContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }
        public async Task<User?> RegisterAsync(UserDto request)
        {
            if (await context.Users.AnyAsync(x => x.UserName == request.UserName))
                return null;
            var user = new User();
            user.UserName = request.UserName;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }


        public async Task<string?> LoginAsync(UserDto request)
        {
            User? user = await context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (user is null)
                return null;
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                        == PasswordVerificationResult.Failed)
                return null;
            string token = createToken(user);
            return token;

        }

        private string createToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName)

            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
