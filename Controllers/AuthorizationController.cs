using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.API.DTO;
using Project.API.Helper;
using Project.API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        private readonly JWTSettings _jwtSettings;
        private readonly IRefreshTokenHandler _refreshTokenHandler;
        public AuthorizationController(DatabaseContext dbContext, IOptions<JWTSettings> options, IRefreshTokenHandler _refreshTokenHandler)
        {
            _dbContext = dbContext;
            _jwtSettings = options.Value;
            this._refreshTokenHandler = _refreshTokenHandler;
        }
        [HttpPost("/generate-access-token")]
        public async Task<IActionResult> GenerateToken([FromBody] UserDTO userDTO)
        {

            var user = await this._dbContext.TblUsers.FirstOrDefaultAsync(item => item.Userid == userDTO.username
                                                                               && item.Password == userDTO.password);
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(this._jwtSettings.secretkey);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Userid),
                        new Claim(ClaimTypes.Role, user.Role)
                    })
                    ,
                    Expires = DateTime.UtcNow.AddSeconds(3000),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var finalToken = tokenHandler.WriteToken(token);
                return Ok( new TokenResponse() { 
                    Token = finalToken, 
                    RefreshToken = await this._refreshTokenHandler.GenerateRefresheToken(userDTO), 
                    UserRole = user.Role 
                });

            }
            else
            {
                return Unauthorized();
            }
            return Ok("");
        }
        [HttpPost("/generate-refresh-token")]
        public async Task<IActionResult> GenerateRefreshToken([FromBody] TokenResponse tokenResponse)
        {
            var _refreshToken = await this._dbContext.TblRefreshtokens.FirstOrDefaultAsync(token => token.RefreshToken == tokenResponse.RefreshToken);
            if(_refreshToken != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(tokenResponse.RefreshToken);

            }
            return Ok("");
        }

    }
}
