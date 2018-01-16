using System.Net;

namespace WhereIsMyBusAPI.Security
{
    /// <summary>
    /// Informações das credenciais de acesso
    /// </summary>
    public class AuthenticationCredentials
    {
        private readonly CookieCollection _cookies;

        public AuthenticationCredentials(CookieCollection cookies)
        {
            _cookies = cookies;
        }

        public bool IsAuthenticated => _cookies.Count > 0;

        public string Cookie
            => IsAuthenticated ? _cookies[0].Value : string.Empty;      
    }
}
