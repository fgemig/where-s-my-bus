using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WhereIsMyBusAPI.Security
{
    /// <summary>
    /// Obtém uma autenticação com a API SPTrans (olhovivo)
    /// </summary>
    public class SPTransAuthentication
    {
        /// <summary>
        /// Realiza uma tentativa de autenticação com a API SPTrans (olhovivo)
        /// </summary>
        /// <param name="url">Url de autenticação</param>
        /// <returns>Retorna um objeto AuthenticationCredentials com as credenciais de acesso</returns>
        public async static Task<AuthenticationCredentials> AuthenticateAsync(string url)
        {
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                var response = await client.PostAsync(url, null);

                var result = await response.Content.ReadAsStringAsync();

                var cookies = new CookieCollection();

                if (bool.TryParse(result, out var success) && success)
                    cookies = handler.CookieContainer.GetCookies(new Uri(url));

                return new AuthenticationCredentials(cookies);
            }
        }
    }
}
