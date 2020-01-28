using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

using VästtrafikUWP.Data;
using VästtrafikUWP.Factories;
using VästtrafikUWP.Models.Departure;
using VästtrafikUWP.Models.Locations;

namespace VästtrafikUWP.ViewModels
{
    class StopDeparturesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DepartureViewModel> Departures = new ObservableCollection<DepartureViewModel>();

        private string time = DateTime.Now.ToShortTimeString().Substring(0, 5);
        public string Time { 
            get => time;
            set
            {
                time = value;
                NotifyPropertyChanged();
            } 
        }

        public StopLocation StopLocation { get; set; }

        public StopDeparturesViewModel(StopLocation _stopLocation)
        {
            StopLocation = _stopLocation;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadDeparturesAsync()
        {
            List<Departure> departures = await ApiService.GetDeparturesListAsync(StopLocation.ID);
            Departures.Clear();

            int counter = 0;
            departures.ForEach(x => 
            {
                Departures.Add(DepartureFactory.DepartureToDepartureViewModel(x));
                counter++;
            });

            Time = DateTime.Now.ToString("HH:mm");
        }
    }
}
