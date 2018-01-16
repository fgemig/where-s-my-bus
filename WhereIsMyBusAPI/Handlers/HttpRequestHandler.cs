using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WhereIsMyBusAPI.Contracts;
using WhereIsMyBusAPI.Helpers;
using WhereIsMyBusAPI.Security;

namespace WhereIsMyBusAPI.Handlers
{
    /// <summary>
    /// Realiza requisições na API da SPTrans
    /// </summary>
    public class HttpRequestHandler : IHttpRequest<HttpRequestHandler>
    {
        private readonly APIConfigurations _configuration;
        private readonly IHttpContextAccessor _context;

        public HttpRequestHandler(APIConfigurations configuration, IHttpContextAccessor context)
        {
            _configuration = configuration;
            _context = context;
        }

        /// <summary>
        /// Cria um request assíncrono na API da SPTrans
        /// </summary>
        /// <typeparam name="T">Classe T para desserialização dos dados</typeparam>
        /// <param name="url">Url para request</param>
        /// <returns>Retorna um objeto T deserializado</returns>
        public async Task<T> CreateGetRequestAsync<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var cookie = _context.HttpContext.User.Claims
                    .Where(c => c.Type == "SPTransCookie").FirstOrDefault();

                var message = HttpRequestHelpers.CreateMessage(string.Concat(_configuration.SPTransBaseUrl, url), cookie.Value);
                
                var response = await httpClient
                     .SendAsync(message)
                     .ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)                
                    throw new UnauthorizedAccessException("Unauthorized");                

                return await HttpRequestHelpers.Deserialize<T>(response);
            }
        }
    }
}
