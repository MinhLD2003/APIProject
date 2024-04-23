using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Project.API.DTO;
using Project.API.Helper;
using System;
using System.Security.Cryptography;

namespace Project.API.Services
{
    public class RefreshTokenHandler : IRefreshTokenHandler
    {
        private readonly DatabaseContext _dbContext;

        public RefreshTokenHandler(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<string> GenerateRefresheToken(UserDTO userDTO)
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
    }
}
