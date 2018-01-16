namespace WhereIsMyBusAPI.Security.Jwt
{
    /// <summary>
    /// Informações para geração do token de acesso
    /// </summary>
    public class JwtConfigurations
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}
