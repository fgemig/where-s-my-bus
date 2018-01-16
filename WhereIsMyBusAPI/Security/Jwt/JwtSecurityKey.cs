using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WhereIsMyBusAPI.Security.Jwt
{
    /// <summary>
    /// Informações da chave de segurança para criação do token de acesso
    /// </summary>
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret) =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
    }
}
