using Newtonsoft.Json;
using PureRadio.Uwp.Models.QingTing.Radio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Radio
{
    public class RadioInfoDetail
    {
        public RadioInfoDetail(
            int radioId, string title, string cover, string description, 
            string audienceCount, string nowplaying, int topCategoryId, 
            string topCategoryTitle, int regionId, int cityId, TimeSpan updateTime)
        {
            RadioId = radioId;
            Title = title;
            Cover = new Uri(cover);
            Description = description;
            AudienceCount = audienceCount;
            Nowplaying = nowplaying;
            TopCategoryId = topCategoryId;
            TopCategoryTitle = topCategoryTitle;
            RegionId = regionId;
            CityId = cityId;
            UpdateTime = updateTime;
        }


        /// <summary>
        /// 电台ID
        /// </summary>
        public int RadioId { get; set; }
        /// <summary>
        /// 电台标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 电台封面图片(URL)
        /// </summary>
        public Uri Cover { get; set; }
        /// <summary>
        /// 电台简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 电台听众计数
        /// </summary>
        public string AudienceCount { get; set; }
        /// <summary>
        /// 正在播放的节目
        /// </summary>
        public string Nowplaying { get; set; }
        /// <summary>
        /// 所属分类Id
        /// </summary>
        public int TopCategoryId { get; set; }
        /// <summary>
        /// 所属分类标题
        /// </summary>
        public string TopCategoryTitle { get; set; }
        /// <summary>
        /// 所属地区Id
        /// </summary>
        public int RegionId { get; set; }
        /// <summary>
        /// 所属城市Id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 需更新时间
        /// </summary>
        public TimeSpan UpdateTime { get; set; }
    }
}
