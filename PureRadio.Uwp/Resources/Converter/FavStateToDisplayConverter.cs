using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace PureRadio.Uwp.Resources.Converter
{
    public class FavStateToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool status = (bool)value;
            var resourceLoader = new ResourceLoader();
            if (status) return resourceLoader.GetString("LangIsFavTrue");
            else return resourceLoader.GetString("LangIsFavFalse");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
