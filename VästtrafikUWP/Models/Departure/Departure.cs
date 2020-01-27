using Newtonsoft.Json;

namespace VästtrafikUWP.Models.Departure
{
    class Departure
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sname")]
        public string Number { get; set; }

        [JsonProperty("journeyNumber")]
        public string JourneyNumber { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("stopid")]
        public string StopID { get; set; }

        [JsonProperty("stop")]
        public string Stop { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("journeyid")]
        public string JourneyID { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("track")]
        public string Track { get; set; }

        [JsonProperty("rtTime")]
        public string RealTime { get; set; }

        [JsonProperty("rtDate")]
        public string RealDate { get; set; }

        [JsonProperty("fgColor")]
        public string ForegroundColor { get; set; }

        [JsonProperty("bgColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("stroke")]
        public string Stroke { get; set; }

        [JsonProperty("accessibility")]
        public string Accessibility { get; set; }

        [JsonProperty("JourneyDetailRef")]
        public JourneyDetailRef JourneyDetailRef { get; set; }
    }
}
