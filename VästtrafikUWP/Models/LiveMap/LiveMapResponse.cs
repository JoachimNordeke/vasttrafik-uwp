using Newtonsoft.Json;

namespace VästtrafikUWP.Models.LiveMap
{
    class LiveMapResponse
    {
        [JsonProperty("livemap")]
        public LiveMap LiveMap { get; set; }
    }
}
