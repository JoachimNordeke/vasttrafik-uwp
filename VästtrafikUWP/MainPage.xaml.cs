using System;
using System.Linq;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

using VästtrafikUWP.Views;
using VästtrafikUWP.ViewModels;
using VästtrafikUWP.Data;
using VästtrafikUWP.Models.Locations;

namespace VästtrafikUWP
{
    public sealed partial class MainPage : Page
    {
        MainPageViewModel viewModel;
        public MainPage()
        {
            ApiService.GetTokenAsync();

            viewModel = new MainPageViewModel();
            this.InitializeComponent();
        }

        private void topbar_Loaded(object sender, RoutedEventArgs e)
        {
            var item = (NavigationViewItemBase)topBar.MenuItems
                .FirstOrDefault(_ => _ is NavigationViewItemBase && (_ as NavigationViewItemBase).Tag.ToString() == "StopInformationPage");
            topBar.SelectedItem = item;
            contentFrame.Navigate(typeof(StopInformation));
        }

        private async void topbar_ItemInvokedAsync(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var itemContent = args.InvokedItem as TextBlock;
            if (itemContent != null)
            {
                switch (itemContent.Tag)
                {
                    case "Nav_StopInformationPage":
                        contentFrame.Navigate(typeof(StopInformation));
                        break;

                    case "Nav_MapPage":
                        contentFrame.Navigate(typeof(MapView));
                        break;

                    case "Nav_StopDeparturesPage":
                        if(viewModel.ChosenStop == null)
                        {
                            await new MessageDialog("Vänligen välj standardhållplats för att visa avgångar.").ShowAsync();
                        }
                        else
                        {
                            contentFrame.Navigate(typeof(StopDepartures), viewModel.ChosenStop);
                        }
                        break;
                }
            }
        }

        private async void StandardStop_TextChangedAsync(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            await Task.Delay(500);

            if (sender.Text.Length > 2 && args.CheckCurrent())
            {
                await viewModel.LoadStopLocationsAsync(sender.Text);
            }
        }

        private void StandardStop_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            viewModel.ChosenStop = (StopLocation)args.SelectedItem;
            if(contentFrame.Content.GetType() == typeof(StopDepartures))
            {
                contentFrame.Navigate(typeof(StopDepartures), viewModel.ChosenStop);
            }
        }
    }
}
