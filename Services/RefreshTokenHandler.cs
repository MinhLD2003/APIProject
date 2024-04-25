using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.API.DTO;
using Project.API.Helper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Project.API.Services
{
    public class RefreshTokenHandler : IRefreshTokenHandler
    {
        private readonly DatabaseContext _dbContext;
        public RefreshTokenHandler(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<string> GenerateRefreshToken(UserDTO userDTO)
        {
            var randomNumber = new byte[64];
            using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                string refreshToken = Convert.ToBase64String(randomNumber);
                var existedToken = this._dbContext.TblRefreshtokens.FirstOrDefaultAsync(item => item.UserId == userDTO.username).Result;

                if (existedToken != null)
                {
                    existedToken.RefreshToken = refreshToken;
                }
                else
                {
                    await this._dbContext.TblRefreshtokens.AddAsync(new Models.TblRefreshtoken
                    {
                        UserId = userDTO.username,
                        TokenId = new Random().Next().ToString(),
                        RefreshToken = refreshToken,
                    });
                }
                await this._dbContext.SaveChangesAsync();
                return refreshToken;
            }
        }
        public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token, string secretKey)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
