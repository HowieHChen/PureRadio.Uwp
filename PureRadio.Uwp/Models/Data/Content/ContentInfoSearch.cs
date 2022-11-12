using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Content
{
    /// <summary>
    /// 内容(专辑)搜索结果视图项
    /// </summary>
    public class ContentInfoSearch
    {
        public ContentInfoSearch(int contentId, string title, string podcaster, string cover, string description, string playCount)
        {
            ContentId = contentId;
            Title = title;
            Podcaster = podcaster;
            Cover = new Uri(cover);
            Description = description;
            PlayCount = playCount;
        }

        /// <summary>
        /// 专辑ID
        /// </summary>
        public int ContentId { get; set; }
        /// <summary>
        /// 专辑标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 主播(专辑所有者)
        /// </summary>
        public string Podcaster { get; set; }
        /// <summary>
        /// 专辑封面图片(URL)
        /// </summary>
        public Uri Cover { get; set; }
        /// <summary>
        /// 专辑简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 专辑播放量
        /// </summary>
        public string PlayCount { get; set; }
    }
}
