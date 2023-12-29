using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechTask.AA.Core.Helpers;

namespace TechTask.AA.Core.Tests.Helpers
{
    [TestFixture]
    public class JwtHelperTests
    {
        [Test]
        public void CreateToken_ShouldCreateTokenCorrectly()
        {
            //Arrange
            var issuer = "https://localhost:7154";
            var audience = "https://localhost:7154";
            var key = "d7801618d9277f736426bd6ce2e8902802eea042ee40da491e05c7d141ebe89e";
            var username = "TestUsername";
            var role = "TestRole";

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new List<Claim>()
                {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, role)
                },
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signinCredentials
            );

            var expectedJWT = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            //Act
            var jwt = JWTHelper.CreateToken(username, role);

            //Assert
            Assert.AreEqual(expectedJWT, jwt);
        }
    }
}
