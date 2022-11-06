using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;

namespace PureRadio.Uwp.Resources.Converter
{
    public class ThemeToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ElementTheme theme = (ElementTheme)value;
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            return theme switch
            {
                ElementTheme.Light => resourceLoader.GetString("PageSettingsThemeLight/Content"),
                ElementTheme.Dark => resourceLoader.GetString("PageSettingsThemeDark/Content"),
                _ => resourceLoader.GetString("PageSettingsThemeSystem/Content"),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
