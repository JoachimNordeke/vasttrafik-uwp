using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;

using VästtrafikUWP.ViewModels;
using VästtrafikUWP.Models.Departure;
using VästtrafikUWP.Models.Locations;

namespace VästtrafikUWP.Views
{
    public sealed partial class StopInformation : Page
    {
        DepartureBoardViewModel viewModel;
        
        public StopInformation()
        {
            viewModel = new DepartureBoardViewModel();
            this.InitializeComponent();
        }

        private async void SearchTextChangedAsync(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            int startLength = textbox.Text.Length;

            await Task.Delay(500);

            if (textbox.Text.Length > 2 && startLength == textbox.Text.Length)
            {
                await viewModel.FindStopsAsync(textbox.Text);
            }
            else if(textbox.Text.Length == 0)
            {
                viewModel.StopResults.Clear();
            }
        }

        private async void stoplocation_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            var location = ((ListView)sender).SelectedItem as StopLocation;

            if(location != null)
            {
                await viewModel.FindDeparturesAsync(location.ID);
            }

        }

        private void departure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var departure = ((ListView)sender).SelectedItem as DepartureViewModel;

            if (departure != null)
            {
                departureInfoFrame.Navigate(typeof(DepartureInfo), departure.Departure);
            }
        }
    }
}
