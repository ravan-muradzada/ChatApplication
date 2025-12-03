using ChatApplication.Application.ExternalServiceInterfaces;
using ChatApplication.Domain.CustomExceptions;
using ChatApplication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Infrastructure.ExternalServices
{
    public class AccessTokenService : IAccessTokenService
    {
        #region Fields
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public AccessTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region SetClaims
        private Claim[] SetClaims(ApplicationUser user)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };
        }
        #endregion

        #region CreateTokenGenerator
        private JwtSecurityToken CreateTokenGenerator(Claim[] claims)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT_EXPIRATION_MINUTES"]));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_Key"] ?? "JUST_EXAMPLE_KEY_FOR_JWT_AUTHENTICATION_WHEN_THE_REAL_KEY_NOT_FOUND"));
            Console.WriteLine(securityKey);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                    _configuration["JWT_Issuer"],
                    _configuration["JWT_Audience"],
                    claims,
                    expires: expiration,
                    signingCredentials: credentials
            );
        }
        #endregion

        #region GenerateToken
        public string GenerateToken(ApplicationUser user)
        {
            Claim[] claims = SetClaims(user);
            JwtSecurityToken tokenGenerator = CreateTokenGenerator(claims);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string accessToken = tokenHandler.WriteToken(tokenGenerator);

            return accessToken;
        }
        #endregion
    }
}
