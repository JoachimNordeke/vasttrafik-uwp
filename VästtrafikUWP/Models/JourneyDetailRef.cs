using Newtonsoft.Json;

namespace VästtrafikUWP.Models
{
    public class JourneyDetailRef
    {
        [JsonProperty("ref")]
        public string Ref { get; set; }
    }
}
