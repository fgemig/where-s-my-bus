namespace WhereIsMyBusAPI.Security
{
    /// <summary>
    /// Informações do Cookie retornado pela API SPTrans (Olhovivo)
    /// </summary>
    public class AuthenticationCookie
    {
        public AuthenticationCookie(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
