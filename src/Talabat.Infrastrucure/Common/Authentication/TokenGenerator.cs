using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Applicaion.Common.Interfaces.Authentication;
using Talabat.Domain.identity;

namespace Talabat.Infrastructure.Common.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtSettings _jwt;
        private readonly UserManager<ApplicationUser> _userManager;
        public TokenGenerator(IOptions<JwtSettings> jwt, UserManager<ApplicationUser> userManager)
        {
            _jwt = jwt.Value;
            _userManager = userManager;
        }
        public async Task<string> GenerateToken(ApplicationUser user, List<string> Roles)
        {
            
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
                .Union(roleClaims); // Add roles as ==> claims

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: credentials                 
            );
            var response = new JwtSecurityTokenHandler().WriteToken(token);

            return response;
            
        }

        public async Task<string> GenerateToken(ApplicationUser user, string Role)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
                .Union(roleClaims); // Add roles as ==> claims

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: credentials
            );
            var response = new JwtSecurityTokenHandler().WriteToken(token);

            return response;
        }
    }
}