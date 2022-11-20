using PureRadio.Uwp.Models.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace PureRadio.Uwp.Resources.Converter
{
    public class RadioCategoryToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var resourceLoader = new ResourceLoader();
            return resourceLoader.GetString(AppConstants.RadioCategoryDict[(int)value]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
