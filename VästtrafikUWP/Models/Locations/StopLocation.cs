using Newtonsoft.Json;

namespace VästtrafikUWP.Models.Locations
{
    public class StopLocation
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("idx")]
        public string IDX { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("track")]
        public string Track { get; set; }
    }
}
