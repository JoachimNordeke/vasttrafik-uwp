using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Windows.Devices.Geolocation;

using VästtrafikUWP.Data;
using VästtrafikUWP.Models.LiveMap;
using VästtrafikUWP.Models.Locations;

namespace VästtrafikUWP.ViewModels
{
    class MapViewViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<StopLocation> NearestStops = new ObservableCollection<StopLocation>();
        public ObservableCollection<Vehicle> LiveVehicles = new ObservableCollection<Vehicle>();

        public Geopoint Position { get; set; }

        private string time;
        public string Time
        {
            get => time;
            set
            {
                time = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateTime()
        {
            Time = DateTime.Now.ToString("HH:mm:ss");
        }

        public async Task LoadUserPositionAsync()
        {
            GeolocationAccessStatus accessStatus = await Geolocator.RequestAccessAsync();

            if(accessStatus == GeolocationAccessStatus.Allowed)
            {
                var locator = new Geolocator();
                Geoposition geoPosition = await locator.GetGeopositionAsync();
                Position = new Geopoint(new BasicGeoposition
                {
                    Latitude = geoPosition.Coordinate.Point.Position.Latitude,
                    Longitude = geoPosition.Coordinate.Point.Position.Longitude
                });
            }
            else
            {
                Position = new Geopoint(new BasicGeoposition
                {
                    Latitude = 57.686614,
                    Longitude = 11.948864
                });
            }
        }

        public async Task LoadNearestStopsAsync()
        {
            List<StopLocation> nearestStops = await ApiService.GetNearbyStopsAsync(Position.Position.Latitude.ToString(), Position.Position.Longitude.ToString());
            NearestStops.Clear();
            nearestStops.ForEach(x => NearestStops.Add(x));
        }

        public async Task LoadLiveVehiclesAsync(double minx, double maxx, double miny, double maxy)
        {
            List<Vehicle> liveVehicles = await ApiService.GetLiveVehiclesAsync(minx, maxx, miny, maxy);
            LiveVehicles.Clear();
            liveVehicles.ForEach(x => LiveVehicles.Add(x));
        }
    }
}
