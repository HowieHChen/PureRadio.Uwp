using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.QingTing.Radio;
using PureRadio.Uwp.Models.QingTing.Recommend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace PureRadio.Uwp.Adapters
{
    public class RadioAdapter : IRadioAdapter
    {
        public RadioInfoDetail ConvertToRadioInfoDetail(RadioDetailItem item)
        {
            var resourceLoader = new ResourceLoader();
            string nowPlaying = item.Nowplaying?.Title ?? resourceLoader.GetString("LangLiveProgramUnknown");
            //if ((item.Nowplaying?.EndTime ?? string.Empty) == "23:59:00") item.Nowplaying.EndTime = "23:59:59";
            TimeSpan timeSpan;
            if (item.Nowplaying != null && DateTime.TryParse(item.Nowplaying.EndTime, out DateTime updateTime))
            {
                if (item.Nowplaying.EndTime == "23:59:00")
                {
                    //item.Nowplaying.EndTime = "23:59:59";
                    updateTime = updateTime.AddSeconds(90);
                }
                timeSpan = updateTime - DateTime.Now;
            }
            else
                timeSpan = TimeSpan.Zero;
            return new RadioInfoDetail(
                item.RadioId, item.Title, item.Cover, item.Description,
                item.AudienceCount.ToString(), nowPlaying, item.TopCategoryId,
                item.TopCategoryTitle, item.RegionId, item.CityId, timeSpan,
                item.Nowplaying?.StartTime ?? string.Empty, item.Nowplaying?.EndTime ?? string.Empty);
        }

        public RadioPlaylistDetail ConvertToRadioPlaylistItem(RadioPlaylistItem item)
        {
            var resourceLoader = new ResourceLoader();
            string boardcasters = resourceLoader.GetString("LangPodcasterUnknown");
            if (item.Broadcasters != null && item.Broadcasters.Count > 0)
            {
                var boardcasterList = item.Broadcasters?.Select(p => $"{p.UserName}").ToList();
                boardcasterList.Sort();
                boardcasters = string.Join(',', boardcasterList);
            }
            //if (item.EndTime == "23:59:00")
            //{
            //    item.EndTime = "23:59:59";
            //}
            return new RadioPlaylistDetail(
                item.StartTime, item.EndTime, item.Duration, item.Day, 
                item.RadioId, item.ProgramId, item.Title, boardcasters);
        }
    }
}
