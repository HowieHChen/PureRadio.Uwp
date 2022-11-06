using PureRadio.Uwp.Models.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PureRadio.Uwp.Resources.Converter
{
    public class LanguageToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string lang = ((string)value).ToLower();
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            return lang switch
            {
                AppConstants.SettingsValue.ZH_CN => resourceLoader.GetString("PageSettingsLangZHCN/Content"),
                AppConstants.SettingsValue.EN_US => resourceLoader.GetString("PageSettingsLangENUS/Content"),
                _ => resourceLoader.GetString("PageSettingsLangSystem/Content"),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
