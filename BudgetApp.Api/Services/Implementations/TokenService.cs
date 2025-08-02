using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces;

namespace BudgetApp.Api.Services.Implementations
{
    /// <summary>
    /// Service implementation for creating JSON Web Tokens (JWT).
    /// </summary>
    public class TokenService(IConfiguration config) : ITokenService
    {

        private readonly SymmetricSecurityKey _key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    Environment.GetEnvironmentVariable("JWT_KEY") 
                    ?? throw new ArgumentNullException("JWT_KEY", "JWT secret key cannot be null.")
                )
            );
        private readonly string? _issuer = config["JwtSettings:Issuer"];

        /// <summary>
        /// Creates a JWT for a given user.
        /// </summary>
        /// <param name="user">The user to create the token for.</param>
        /// <returns>The generated JWT string.</returns>
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, user.User_Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Username),
                new(JwtRegisteredClaimNames.Email, user.Email)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Token is valid for 7 days
                SigningCredentials = creds,
                Issuer = _issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
