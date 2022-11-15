using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.UI.Xaml.Data;

namespace PureRadio.Uwp.Resources.Converter
{
    public class MediaPlaybackStateToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (MediaPlaybackState)value switch
            {
                MediaPlaybackState.None => "\xF5B0",
                MediaPlaybackState.Opening => "\xF5B0",
                MediaPlaybackState.Buffering => "\xF8AE",
                MediaPlaybackState.Playing => "\xF8AE",
                MediaPlaybackState.Paused => "\xF5B0",
                _ => "\xF5B0",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
