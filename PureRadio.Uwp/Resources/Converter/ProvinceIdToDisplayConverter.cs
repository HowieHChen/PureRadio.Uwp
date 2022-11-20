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
    public class ProvinceIdToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if((int)value == 0) return string.Empty;
            var resourceLoader = new ResourceLoader();
            return AppConstants.ProvinceIdDict[(int)value] + " " + resourceLoader.GetString("PageRadioLocalTrend");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
