using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Radio
{
    public class RadioInfoRecommend
    {
        public RadioInfoRecommend(
            string cover, string title, string startTime, 
            string endTime, string nowplaying, int radioId)
        {
            Cover = cover;
            Title = title;
            StartTime = startTime;
            EndTime = endTime;
            Nowplaying = nowplaying;
            RadioId = radioId;
        }

        /// <summary>
        /// 电台封面图片(URL)
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 电台标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 当前节目开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 当前节目结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 正在播放的节目
        /// </summary>
        public string Nowplaying { get; set; }
        /// <summary>
        /// 电台ID
        /// </summary>
        public int RadioId { get; set; }
    }
}
