using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WhereIsMyBusAPI.Security;
using WhereIsMyBusAPI.Security.Jwt;

namespace WhereIsMyBusAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly JwtConfigurations _jwtConfigurations;
        private readonly APIConfigurations _ApiConfigurations;

        public AccountController(
            JwtConfigurations jwtConfigurations,
            APIConfigurations apiConfigurations)
        {
            _jwtConfigurations = jwtConfigurations;
            _ApiConfigurations = apiConfigurations;
        }

        /// <summary>
        /// Autenticação com a API
        /// </summary>
        /// <returns>Retorna um objeto com o token de acesso</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var credentials = await AuthenticateAsync();

            if (!credentials.IsAuthenticated)
            {
                return BadRequest(new
                {
                    authenticated = false,
                    message = "Not authenticated with SPTrans API"
                });
            }

            var jwtToken = new JwtTokenBuilder(_jwtConfigurations)                              
                              .AddClaim("SPTransCookie", credentials.Cookie)                              
                              .Build();

            return Ok(new
            {
                authenticated = true,
                token = jwtToken
            });
        }

        private async Task<AuthenticationCredentials> AuthenticateAsync()
        {
            var url = string.Concat(_ApiConfigurations.SPTransBaseUrl, "/Login/Autenticar?token=", _ApiConfigurations.SpTransApiKey);

            return await SPTransAuthentication
                       .AuthenticateAsync(url)
                       .ConfigureAwait(false);
        }
    }
}