using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TechTask.AA.Core.Helpers;

public static class JWTHelper
{
    public static class Constants
    {
        public const string Issuer = "https://localhost:7154";
        public const string Audience = "https://localhost:7154";
        public const string Key = "d7801618d9277f736426bd6ce2e8902802eea042ee40da491e05c7d141ebe89e";
    }

    public static string CreateToken(string username, string role)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Key));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: Constants.Issuer,
            audience: Constants.Audience,
            claims: new List<Claim>()
            {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
            },
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: signinCredentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return jwt;
    }
}
