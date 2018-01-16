using Newtonsoft.Json;

namespace WhereIsMyBusAPI.Models.Responses
{
    /// <summary>
    /// Informações das linhas de ônibus
    /// </summary>
    public class LineResponse
    {
        [JsonProperty(PropertyName = "cl")]
        public string LineCode { get; set; }

        [JsonProperty(PropertyName = "sl")]
        public byte Direction { get; set; }

        [JsonProperty(PropertyName = "tp")]
        public string PrincipalStation { get; set; }

        [JsonProperty(PropertyName = "ts")]
        public string SecundaryStation { get; set; }

        public string Description => Direction == 1
                    ? string.Concat("Sentido => ", SecundaryStation)
                    : string.Concat("Sentido => ", PrincipalStation);
    }
}
