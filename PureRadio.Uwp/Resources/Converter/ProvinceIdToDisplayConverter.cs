using PureRadio.Uwp.Models.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;
using static SQLite.SQLite3;

namespace PureRadio.Uwp.Resources.Converter
{
    public class ProvinceIdToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if((int)value == 0) return string.Empty;
            if (!AppConstants.ProvinceDict.TryGetValue((int)value, out string region))
                region = "LangProvinceUnknown";
            var resourceLoader = new ResourceLoader();
            return resourceLoader.GetString(region) + " " + resourceLoader.GetString("PageRadioLocalTrend");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
