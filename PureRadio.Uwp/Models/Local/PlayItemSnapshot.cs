using PureRadio.Uwp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Local
{
    public class PlayItemSnapshot
    {
        public PlayItemSnapshot(
            MediaPlayType type, Uri sourceUri, Uri cover,
            string title, string subTitle, int mainId, int secondaryId, int duration,
            string startTime = "", string endTime = "", DayOfWeek dayOfWeek = DayOfWeek.Sunday)
        {
            Type = type;
            SourceUri = sourceUri;
            Cover = cover;
            Title = title;
            SubTitle = subTitle;
            MainId = mainId;
            SecondaryId = secondaryId;
            Duration = duration;
            StartTime = startTime;
            EndTime = endTime;
            DayOfWeek = dayOfWeek;
        }

        /// <summary>
        /// 播放类型
        /// </summary>
        public MediaPlayType Type { get; set; }
        /// <summary>
        /// 主要ID
        /// </summary>
        public int MainId { get; }
        /// <summary>
        /// 次要ID
        /// </summary>
        public int SecondaryId { get; }
        /// <summary>
        /// 音源URL
        /// </summary>
        public Uri SourceUri { get; }
        /// <summary>
        /// 封面URL
        /// </summary>
        public Uri Cover { get; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; }
        /// <summary>
        /// 总时长
        /// </summary>
        public int Duration { get; }
        /// <summary>
        /// 开始时间(电台直播)
        /// </summary>
        public string StartTime { get; }
        /// <summary>
        /// 结束时间(电台直播)
        /// </summary>
        public string EndTime { get; }
        /// <summary>
        /// 周几的节目(电台回放)
        /// </summary>
        public DayOfWeek DayOfWeek { get; }
    }
}
