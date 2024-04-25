using Project.API.DTO;
using System.Security.Claims;

namespace Project.API.Services
{
    public interface IRefreshTokenHandler
    {
        Task<string> GenerateRefreshToken(UserDTO userDTO);
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token ,string secretKey);
    }
}
