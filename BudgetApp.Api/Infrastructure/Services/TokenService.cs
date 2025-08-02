using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BudgetApp.Api.Core.Entities;
using BudgetApp.Api.Core.Interfaces.Services;
using BudgetApp.Api.Core.Constants;

namespace BudgetApp.Api.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly string _issuer;

    public TokenService(IConfiguration config)
    {
        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") 
                     ?? throw new ArgumentNullException("JWT_KEY", "JWT secret key cannot be null.");
        
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        _issuer = config["JwtSettings:Issuer"] 
                  ?? throw new ArgumentNullException("JwtSettings:Issuer", "JWT issuer cannot be null.");
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ApplicationConstants.Claims.UserId, user.UserId.ToString()),
            new(ApplicationConstants.Claims.Username, user.Username),
            new(ApplicationConstants.Claims.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, 
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), 
                ClaimValueTypes.Integer64)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(ApplicationConstants.Authentication.TokenExpirationDays),
            SigningCredentials = creds,
            Issuer = _issuer,
            Audience = _issuer
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}