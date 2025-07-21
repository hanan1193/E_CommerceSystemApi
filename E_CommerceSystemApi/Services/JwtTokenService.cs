using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_CommerceSystemApi.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace E_CommerceSystemApi.Services
{
    public class JwtTokenService
    {
        private readonly JWTSettings _jwt;
        public JwtTokenService(IOptions<JWTSettings> jwtOptions)
        {
           _jwt = jwtOptions.Value;
        }
        public string GenerateToken(string userId, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.Role,role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
              issuer: _jwt.Issuer,
              audience: _jwt.Audience,
              claims: claims,
              expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
              signingCredentials: creds
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
       
    }
}
