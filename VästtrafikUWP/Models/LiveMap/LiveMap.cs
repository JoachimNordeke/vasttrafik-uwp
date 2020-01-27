using System.Collections.Generic;

using Newtonsoft.Json;

namespace VästtrafikUWP.Models.LiveMap
{
    class LiveMap
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("maxx")]
        public double Maxx { get; set; }

        [JsonProperty("maxy")]
        public double Maxy { get; set; }

        [JsonProperty("minx")]
        public double Minx { get; set; }

        [JsonProperty("miny")]
        public double Miny { get; set; }

        [JsonProperty("vehicles")]
        public List<Vehicle> Vehicles { get; set; }
    }
}
