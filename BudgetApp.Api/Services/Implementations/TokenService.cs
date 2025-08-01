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
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;

        public TokenService(IConfiguration config)
        {
            // Get secret key and issuer from appsettings.json
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Secret"]));
            _issuer = config["JwtSettings:Issuer"];
        }

        /// <summary>
        /// Creates a JWT for a given user.
        /// </summary>
        /// <param name="user">The user to create the token for.</param>
        /// <returns>The generated JWT string.</returns>
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.user_id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.username),
                new Claim(JwtRegisteredClaimNames.Email, user.email)
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
