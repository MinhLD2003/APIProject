using Project.API.DTO;

namespace Project.API.Services
{
    public interface IRefreshTokenHandler
    {
        Task<string> GenerateRefresheToken(UserDTO userDTO);
    }
}
