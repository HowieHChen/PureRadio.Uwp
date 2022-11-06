using PureRadio.Uwp.Models.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PureRadio.Uwp.Resources.Converter
{
    public class LanguageToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((string)value).ToLower() switch
            {
                AppConstants.SettingsValue.ZH_CN => 1,
                AppConstants.SettingsValue.EN_US => 2,
                _ => 0,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (int)value switch
            {
                1 => AppConstants.SettingsValue.ZH_CN,
                2 => AppConstants.SettingsValue.EN_US,
                _ => AppConstants.SettingsValue.Auto,
            };
        }
    }
}
