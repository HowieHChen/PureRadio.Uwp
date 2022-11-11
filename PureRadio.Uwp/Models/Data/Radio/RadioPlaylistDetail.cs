using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Radio
{
    public class RadioPlaylistDetailSet
    {
        public List<RadioPlaylistDetail> BYday { get; set; }
        public List<RadioPlaylistDetail> Yday { get; set; }
        public List<RadioPlaylistDetail> Today { get; set; }
        public List<RadioPlaylistDetail> Tmr { get; set; }
    }

    public class RadioPlaylistDetail
    {
        public RadioPlaylistDetail(
            string startTime, string endTime, int duration, int day,
            int radioId, int progrmaId, string title, string broadcasters)
        {
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            Day = day;
            RadioId = radioId;
            ProgramId = progrmaId;
            Title = title;
            Broadcasters = broadcasters;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 节目时长
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 星期几的节目(1->Sunday,2->Monday,etc.)
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 节目所属电台的ID
        /// </summary>
        public int RadioId { get; set; }
        /// <summary>
        /// 节目的ID
        /// </summary>
        public int ProgramId { get; set; }
        /// <summary>
        /// 节目标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 主播(可能有多个)
        /// </summary>
        public string Broadcasters { get; set; }
    }
}
