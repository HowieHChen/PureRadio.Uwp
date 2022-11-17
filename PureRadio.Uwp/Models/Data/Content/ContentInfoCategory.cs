using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Content
{
    public class ContentInfoCategory
    {
        public ContentInfoCategory(
            int contentId, string title, string description, 
            string cover, float rating, string playCount)
        {
            ContentId = contentId;
            Title = title;
            Description = description;
            Cover = cover;
            Rating = rating;
            PlayCount = playCount;
        }


        /// <summary>
        /// 内容(专辑)Id
        /// </summary>
        public int ContentId { get; set; }
        /// <summary>
        /// 内容(专辑)标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容(专辑)简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 内容(专辑)封面图片(URL)
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 内容(专辑)评分 (0 ~ 10)
        /// </summary>
        public float Rating { get; set; }
        /// <summary>
        /// 内容(专辑)播放量
        /// </summary>
        public string PlayCount { get; set; }
    }
}
