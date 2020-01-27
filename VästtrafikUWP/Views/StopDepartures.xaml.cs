using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using VästtrafikUWP.Models.Locations;
using VästtrafikUWP.ViewModels;

namespace VästtrafikUWP.Views
{
    public sealed partial class StopDepartures : Page
    {
        StopDeparturesViewModel viewModel;
        
        public StopDepartures()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            viewModel = new StopDeparturesViewModel(e.Parameter as StopLocation);
            await LoadDepartureLoopAsync();
        }

        //Reloads departures every 15 seconds.
        //At first I used a recursive method, but that would, in time, lead to stack overflow exception.
        public async Task LoadDepartureLoopAsync() {
            while (true) {
                await viewModel.LoadDeparturesAsync();
                await Task.Delay(15000);
            }
        }
    }
}
