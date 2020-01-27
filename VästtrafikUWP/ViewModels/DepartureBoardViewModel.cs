using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using VästtrafikUWP.Data;
using VästtrafikUWP.Models.Departure;
using VästtrafikUWP.Models.Locations;

namespace VästtrafikUWP.ViewModels
{
    class DepartureBoardViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DepartureViewModel> Departures = new ObservableCollection<DepartureViewModel>();
        public ObservableCollection<StopLocation> StopResults = new ObservableCollection<StopLocation>();
        
        private Departure departureInfo;
        public Departure DepartureInfo
        {
            get => departureInfo;
            set {
                departureInfo = value;
                NotifyPropertyChanged("DepartureInfo");
            }
        }

        public string SearchText = "";

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task FindStopsAsync(string searchText)
        {
            var stopLocations = await ApiService.GetStopLocationsAsync(searchText);

            StopResults.Clear();
            foreach (var stop in stopLocations)
            {
                StopResults.Add(stop);
            }
        }

        public async Task FindDeparturesAsync(string stopId)
        {
            var departures = await ApiService.GetDeparturesListAsync(stopId);

            Departures.Clear();
            foreach(var departure in departures)
            {
                var date = departure.Date;
                var realdate = departure.RealDate;
                var time = departure.Time;
                var realtime = departure.RealTime;

                var departureTime = DateTime.Parse((realdate != null ? realdate : date) + " " + (realtime != null ? realtime : time) + ":00");
                var actualTime = DateTime.Now;

                TimeSpan diff = departureTime - actualTime;

                int minutesLeft = Convert.ToInt32(Math.Round(diff.TotalMinutes, 0));
                

                Departures.Add(new DepartureViewModel 
                { 
                    Departure = departure,
                    MinutesLeft = minutesLeft + " min"
                });
            }
        }

        public void SetDepartureInfo(Departure departure)
        {
            DepartureInfo = departure;
        }
    }
}
