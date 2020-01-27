using System;
using System.Linq;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

using VästtrafikUWP.ViewModels;

namespace VästtrafikUWP.Views
{
    public sealed partial class MapView : Page
    {
        MapViewViewModel viewModel;

        public MapView()
        {
            viewModel = new MapViewViewModel();
            this.InitializeComponent();
        }

        private async void MapControl_LoadingAsync(FrameworkElement sender, object args)
        {
            await viewModel.LoadUserPositionAsync();
            var myPOI = new MapIcon { Location = viewModel.Position, Title = "Din position" };

            Map.MapElements.Add(myPOI);
            Map.Center = viewModel.Position;
            Map.ZoomLevel = 17;

            LoadLiveVehiclesAsync();
        }

        private async void LoadLiveVehiclesAsync()
        {
            while (true)
            {
                var positions = Map.GetVisibleRegion(MapVisibleRegionKind.Near).Positions.ToList();

                double minx = Math.Round(positions[6].Longitude * 1000000, 0);
                double maxx = Math.Round(positions[3].Longitude * 1000000, 0);
                double miny = Math.Round(positions[3].Latitude * 1000000, 0);
                double maxy = Math.Round(positions[6].Latitude * 1000000, 0);

                await viewModel.LoadLiveVehiclesAsync(minx, maxx, miny, maxy);

                await Task.Delay(5000);
            }
        }
    }
}
