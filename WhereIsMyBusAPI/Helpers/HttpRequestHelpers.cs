using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WhereIsMyBusAPI.Helpers
{
    /// <summary>
    /// Helpers para as classes de Http Request
    /// </summary>
    public class HttpRequestHelpers
    {
        /// <summary>
        /// Deserializa os dados em um objeto T
        /// </summary>
        /// <typeparam name="T">Classe T para deserialização dos dados</typeparam>
        /// <param name="response">Objeto HttpResponseMessage</param>
        /// <returns>Retorna um objeto deserializado</returns>
        public async static Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            var json = await response
                .Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Cria uma mensagem de request
        /// </summary>
        /// <param name="url">Url de request</param>
        /// <param name="cookie">Objeto AuthenticationCookie com dados da autenticação</param>
        /// <returns>Retorna um objeto  HttpRequestMessage com o cabeçalho</returns>
        public static HttpRequestMessage CreateMessage(string url, string cookie)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, url);
            message.Headers.Add("Cookie", "apiCredentials=" + cookie);
            return message;
        }
    }
}
