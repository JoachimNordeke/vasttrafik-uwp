using Newtonsoft.Json;

namespace VästtrafikUWP.Models.Locations
{
    class LocationResponse
    {
        [JsonProperty("LocationList")]
        public LocationList LocationList { get; set; }
    }
}
