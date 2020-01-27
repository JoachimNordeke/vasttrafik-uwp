using Newtonsoft.Json;

namespace VästtrafikUWP.Models.Locations
{
    class CoordLocation
    {
        [JsonProperty("idx")]
        public string IDX { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }
    }
}
