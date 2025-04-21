using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WEBApiREST.Interfaces;
namespace WEBApiREST
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        public string GenerateToken(UserEntity user)
        {
            Claim[] claims = [new("userName", user.username.ToString())];

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public string GetUserFromToken(string token) 
        {
            List<Claim> source = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.ToList();
            string? userName = source.SingleOrDefault((Claim claim) => claim.Type == "preferred_username")?.Value;
            //string obj = source.SingleOrDefault((Claim claim) => claim.Type == "employee_id")?.Value;

            return (userName);
        }
    }
}
