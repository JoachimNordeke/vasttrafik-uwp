using Newtonsoft.Json;

using System.Collections.Generic;

namespace VästtrafikUWP.Models.Departure
{
    class DepartureBoard
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("servertime")]
        public string ServerTime { get; set; }

        [JsonProperty("serverdate")]
        public string ServerDate { get; set; }

        [JsonProperty("Departure")]
        public List<Departure> DepartureList { get; set; }
    }
}
