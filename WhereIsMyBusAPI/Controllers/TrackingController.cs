using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhereIsMyBusAPI.Contracts;
using WhereIsMyBusAPI.Handlers;
using WhereIsMyBusAPI.Models.Responses;

namespace WhereIsMyBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TrackingController : Controller
    {
        private readonly IHttpRequest<HttpRequestHandler> _httpRequest;

        public TrackingController(IHttpRequest<HttpRequestHandler> httpRequest)
        {
            _httpRequest = httpRequest;
        }

        /// <summary>
        /// Retorna a posição exata de cada veículo da linha informada
        /// </summary>
        /// <param name="lineCode">Código da linha</param>
        /// <returns>Retorna uma lista completa com a última localização de todos os veículos mapeados com suas devidas posições</returns>
        [HttpGet("GetByLineCode")]
        public async Task<IActionResult> GetByLineCodeAsync(string lineCode)
        {
            var url = "/Posicao/Linha?codigoLinha=" + lineCode;

            try
            {
                var response = await _httpRequest.CreateGetRequestAsync<TrackingResponse>(url);

                var result = response
                    .Vehicles
                        .Select(c => new
                        {
                            c.Prefix,
                            c.IsAccessible,
                            c.Latitude,
                            c.Longitude
                        });

                return Json(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}