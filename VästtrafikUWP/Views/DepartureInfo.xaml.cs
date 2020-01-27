using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using VästtrafikUWP.Models.Departure;

namespace VästtrafikUWP.Views
{
    public sealed partial class DepartureInfo : Page
    {
        Departure departure;

        public DepartureInfo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            departure = e.Parameter as Departure;
        }
    }
}
