using JwtTokenProject.Entity;
using JwtTokenProject.Model;

namespace JwtTokenProject.Services
{
    public interface IAuthServices
    {
        Task<string?> LoginAsync(UserDto request);
        Task<User?> RegisterAsync(UserDto request);
    }
}