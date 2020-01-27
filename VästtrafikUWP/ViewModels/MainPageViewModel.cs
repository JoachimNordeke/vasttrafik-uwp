using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using VästtrafikUWP.Data;
using VästtrafikUWP.Models.Locations;

namespace VästtrafikUWP.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<StopLocation> StopLocations = new ObservableCollection<StopLocation>();

        private StopLocation chosenStop;
        public StopLocation ChosenStop {
            get => chosenStop;
            set
            {
                chosenStop = value;
                NotifyPropertyChanged();
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadStopLocationsAsync(string text)
        {
            var locations = await ApiService.GetStopLocationsAsync(text);
            StopLocations.Clear();
            locations?.ForEach(x => StopLocations.Add(x));
        }
    }
}
