using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace WhereIsMyBusAPI.Security.Jwt
{
    /// <summary>
    /// Cria um token JWT
    /// </summary>
    public class JwtTokenBuilder
    {
        private readonly JwtConfigurations _jwtConfigurations;
        private readonly Dictionary<string, string> claims;

        public JwtTokenBuilder(JwtConfigurations jwtConfigurations)
        {
            _jwtConfigurations = jwtConfigurations;
            claims = new Dictionary<string, string>();
        }

        public JwtTokenBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        public string Build()
        {
            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }

            .Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                              issuer: _jwtConfigurations.Issuer,
                              audience: _jwtConfigurations.Audience,
                              claims: claims,
                              expires: DateTime.UtcNow.AddHours(5),
                              signingCredentials: new SigningCredentials(
                                                       JwtSecurityKey.Create(_jwtConfigurations.Key),
                                                       SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
