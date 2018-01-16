using Newtonsoft.Json;
using System.Collections.Generic;

namespace WhereIsMyBusAPI.Models.Responses
{
    /// <summary>
    /// Informações do rastreamento
    /// </summary>
    public class TrackingResponse
    {
        [JsonProperty(PropertyName = "hr")]
        public string Hour { get; set; }

        [JsonProperty(PropertyName = "vs")]
        public IList<VehicleResponse> Vehicles { get; set; }
    }
}
