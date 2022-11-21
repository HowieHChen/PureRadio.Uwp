using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Radio
{
    public class RadioReplayInfo
    {
        public RadioReplayInfo(
            string cover, string title, int playcount, 
            int contentId, string radioTitle, string category)
        {
            Cover = new Uri(cover);
            Title = title;
            Playcount = playcount;
            ContentId = contentId;
            RadioTitle = radioTitle;
            Category = category;
        }


        /// <summary>
        /// 电台节目封面图片(URL)
        /// </summary>
        public Uri Cover { get; set; }
        /// <summary>
        /// 电台节目标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 电台节目播放量
        /// </summary>
        public int Playcount { get; set; }
        /// <summary>
        /// 专辑Id
        /// </summary>
        public int ContentId { get; set; }
        /// <summary>
        /// 电台节目所属电台的标题
        /// </summary>
        public string RadioTitle { get; set; }
        /// <summary>
        /// 分类(可能为空)
        /// </summary>
        public string Category { get; set; }
    }
}
