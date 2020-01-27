using System;

using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace VästtrafikUWP.Converters
{
    class VehicleIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value != null)
            {
                switch (value as string)
                {
                    case "TRAM":
                        return new BitmapImage(new Uri("ms-appx:///Assets/tramIcon.png"));
                    case "BUS":
                        return new BitmapImage(new Uri("ms-appx:///Assets/busIcon.png"));
                    case "BOAT":
                        return new BitmapImage(new Uri("ms-appx:///Assets/boatIcon.png"));
                    default:
                        return new BitmapImage(new Uri("ms-appx:///Assets/defaultIcon.png"));
                }
            }
            return new BitmapImage(new Uri("ms-appx:///Assets/defaultIcon.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
