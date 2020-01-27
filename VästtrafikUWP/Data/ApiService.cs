using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

using VästtrafikUWP.Models.Departure;
using VästtrafikUWP.Models.Authentication;
using VästtrafikUWP.Models.Locations;
using VästtrafikUWP.Models.LiveMap;

namespace VästtrafikUWP.Data
{
    static class ApiService
    {
        // This needs to be stored in a more secure way.
        private const string clientID = "tdIt1TWz4bUv7KTKOWjqE_fZuDwa";
        private const string clientSecret = "WORuN2bEkujcef4Zfwse7VZZVusa";

        private const string baseUrl = "https://api.vasttrafik.se/bin/rest.exe/v2";
        private static string token = string.Empty;

        public static async void GetTokenAsync()
        {
            var tokenUrl = $"https://api.vasttrafik.se:443/token?grant_type=client_credentials&client_id={clientID}&client_secret={clientSecret}";
            
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(tokenUrl, null);
                var json = response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<TokenResponse>(json.Result).Token;
            }
        }

        public static async Task<List<Departure>> GetDeparturesListAsync(string stopId = "9021014001150000")
        {
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToString("HH:mm").Replace(":", "%3A");

            string url = baseUrl + $"/departureBoard?id={stopId}&date={date}&time={time}&format=json";

            var departureBoardResponse = JsonConvert.DeserializeObject<DepartureboardResponse>(await RequestJsonAsync(url));
            return departureBoardResponse.DepartureBoard.DepartureList
                .Where(x => ExtractDepartureDateTime(x) > ExtractServerDateTime(departureBoardResponse.DepartureBoard))
                .OrderBy(x => x.RealTime != null ? x.RealTime : x.Time)
                .ToList();
        }

        public static async Task<List<Vehicle>> GetLiveVehiclesAsync(double minx, double maxx, double miny, double maxy)
        {
            var url = baseUrl + $"/livemap?minx={minx}&maxx={maxx}&miny={miny}&maxy={maxy}&onlyRealtime=no";

            return JsonConvert.DeserializeObject<LiveMapResponse>(await RequestJsonAsync(url)).LiveMap.Vehicles;
        }

        public static async Task<List<StopLocation>> GetStopLocationsAsync(string searchText)
        {
            var url = baseUrl + $"/location.name?format=json&input={searchText}";
            return await GetLocationResponseAsync(url);
        }

        public static async Task<List<StopLocation>> GetNearbyStopsAsync(string latitude, string longitude)
        {
            var url = baseUrl + $"/location.nearbystops?format=json&originCoordLat={latitude}&originCoordLong={longitude}";
            return await GetLocationResponseAsync(url);
        }

        private static async Task<List<StopLocation>> GetLocationResponseAsync(string url)
        {
            var locationResponse = JsonConvert.DeserializeObject<LocationResponse>(await RequestJsonAsync(url));
            return locationResponse.LocationList.StopLocations;
        }

        private static async Task<string> RequestJsonAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(url);
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        private static DateTime ExtractDepartureDateTime(Departure departure)
        {
            return new DateTime(
                int.Parse((departure.RealDate != null ? departure.RealDate : departure.Date).Substring(0, 4)),
                int.Parse((departure.RealDate != null ? departure.RealDate : departure.Date).Substring(5, 2)),
                int.Parse((departure.RealDate != null ? departure.RealDate : departure.Date).Substring(8, 2)),
                int.Parse((departure.RealTime != null ? departure.RealTime : departure.Time).Substring(0, 2)),
                int.Parse((departure.RealTime != null ? departure.RealTime : departure.Time).Substring(3, 2)),
                0);
        }
        private static DateTime ExtractServerDateTime(DepartureBoard board)
        {
            return new DateTime(
                int.Parse(board.ServerDate.Substring(0, 4)),
                int.Parse(board.ServerDate.Substring(5, 2)),
                int.Parse(board.ServerDate.Substring(8, 2)),
                int.Parse(board.ServerTime.Substring(0, 2)),
                int.Parse(board.ServerTime.Substring(3, 2)),
                0);
        }
    }
}
