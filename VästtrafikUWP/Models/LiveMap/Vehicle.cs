using Windows.Devices.Geolocation;

using Newtonsoft.Json;

namespace VästtrafikUWP.Models.LiveMap
{
    class Vehicle
    {
        [JsonProperty("lcolor")]
        public string LineColor { get; set; }

        [JsonProperty("prodClass")]
        public string ProductClass { get; set; }

        [JsonProperty("bcolor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("direction")]
        public int Direction { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gid")]
        public string ServiceGID { get; set; }

        [JsonProperty("delay")]
        public int Delay { get; set; }

        [JsonProperty("y")]
        public double Latitude { get; set; }

        [JsonProperty("x")]
        public double Longitude { get; set; }

        public Geopoint Location
        {
            get => new Geopoint(new BasicGeoposition { Latitude = Latitude / 1000000, Longitude = Longitude / 1000000 });
        }
    }
}
