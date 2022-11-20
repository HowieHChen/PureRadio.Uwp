using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Content
{
    public class ContentInfoRecommend
    {
        public ContentInfoRecommend(
            string cover, string title, string recWords, 
            string playCount, float score, int contentId)
        {
            Cover = cover;
            Title = title;
            RecWords = recWords;
            PlayCount = playCount;
            Score = score;
            ContentId = contentId;
        }


        /// <summary>
        /// 内容(专辑)封面图片(URL)
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 内容(专辑)标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 推荐词
        /// </summary>
        public string RecWords { get; set; }
        /// <summary>
        /// 内容(专辑)播放量
        /// </summary>
        public string PlayCount { get; set; }
        /// <summary>
        /// 内容(专辑)评分 (0 ~ 5)
        /// </summary>
        public float Score { get; set; }
        /// <summary>
        /// 内容(专辑)Id
        /// </summary>
        public int ContentId { get; set; }
    }
}
