using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WhereIsMyBusAPI.Handlers;
using WhereIsMyBusAPI.Contracts;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System;
using WhereIsMyBusAPI.Models.Responses;

namespace WhereIsMyBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LineController : Controller
    {
        private readonly IHttpRequest<HttpRequestHandler> _httpRequest;

        public LineController(IHttpRequest<HttpRequestHandler> httpRequest)
        {
            _httpRequest = httpRequest;
        }

        /// <summary>
        /// Possibilita a consulta pelas linhas de ônibus da cidade de São Paulo, bem como suas informações cadastrais     
        /// </summary>
        /// <param name="prefix">Prefixo da linha. Ex: 675K-10</param>
        /// <returns>Retorna uma lista de ônibus a partir de um determinado prefixo ou itinerário</returns>        
        [HttpGet("GetByPrefix")]
        public async Task<IActionResult> GetByPrefixAsync(string prefix)
        {
            var url = "/Linha/Buscar?termosBusca=" + prefix;

            try
            {
                var response = await _httpRequest.CreateGetRequestAsync<IList<LineResponse>>(url);

                var result = response
                     .Select(c => new
                     {
                         c.LineCode,
                         c.Description
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