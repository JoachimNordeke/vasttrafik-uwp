using System;

using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace VästtrafikUWP.Converters
{
    class AlternateColorConverter : IValueConverter
    {
        private int counter = 0;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = (Color)XamlBindingHelper.ConvertValue(typeof(Color), counter % 2 == 0 ? "#333333" : "#000000");
            counter++;
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
