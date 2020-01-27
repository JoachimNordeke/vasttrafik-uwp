using System.Collections.Generic;

using Newtonsoft.Json;

namespace VästtrafikUWP.Models.Locations
{
    class LocationList
    {
        [JsonProperty("servertime")]
        public string ServerTime { get; set; }

        [JsonProperty("serverdate")]
        public string ServerDate { get; set; }

        [JsonProperty("errorText")]
        public string ErrorText { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("StopLocation")]
        public List<StopLocation> StopLocations { get; set; }

        [JsonProperty("CoordLocation")]
        public List<CoordLocation> CoordLocations { get; set; }
    }
}
