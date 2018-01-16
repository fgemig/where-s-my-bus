using Newtonsoft.Json;

namespace WhereIsMyBusAPI.Models.Responses
{
    /// <summary>
    /// Informações dos veículos
    /// </summary>
    public class VehicleResponse
    {
        [JsonProperty(PropertyName = "p")]
        public string Prefix { get; set; }

        [JsonProperty(PropertyName = "a")]
        public bool IsAccessible { get; set; }

        [JsonProperty(PropertyName = "px")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "py")]
        public double Longitude { get; set; }
    }
}
