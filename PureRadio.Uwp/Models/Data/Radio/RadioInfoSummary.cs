using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Radio
{
    public class RadioInfoSummary
    {
        public RadioInfoSummary(
            int radioId, Uri cover, string title, 
            string nowplaying, string description, string audienceCount)
        {
            RadioId = radioId;
            Cover = cover;
            Title = title;
            Nowplaying = nowplaying;
            Description = description;
            AudienceCount = audienceCount;
        }

        /// <summary>
        /// 电台ID
        /// </summary>
        public int RadioId { get; set; }
        /// <summary>
        /// 电台封面图片(URL)
        /// </summary>
        public Uri Cover { get; set; }
        /// <summary>
        /// 电台标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 正在播放的节目
        /// </summary>
        public string Nowplaying { get; set; }
        /// <summary>
        /// 电台简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 电台听众计数
        /// </summary>
        public string AudienceCount { get; set; }
    }
}
