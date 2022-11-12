using Newtonsoft.Json;
using PureRadio.Uwp.Models.QingTing.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Content
{
    public class ContentInfoDetail
    {
        public ContentInfoDetail(
            Uri cover, int contentId, string version, string title, 
            string description, int programCount, string playCount, 
            float rating, string podcasters, int categoryId, string contentType, 
            List<AttributesItem> attributes = null)
        {
            Cover = cover;
            ContentId = contentId;
            Version = version;
            Title = title;
            Description = description;
            ProgramCount = programCount;
            PlayCount = playCount;
            Rating = rating;
            Podcasters = podcasters;
            CategoryId = categoryId;
            ContentType = contentType;
            Attributes = attributes;
        }


        /// <summary>
        /// 内容(专辑)封面图片(URL)
        /// </summary>
        public Uri Cover { get; set; }
        /// <summary>
        /// 内容(专辑)Id
        /// </summary>
        public int ContentId { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 内容(专辑)标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容(专辑)简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 内容(专辑)所包含的节目个数
        /// </summary>
        public int ProgramCount { get; set; }
        /// <summary>
        /// 内容(专辑)播放量
        /// </summary>
        public string PlayCount { get; set; }
        /// <summary>
        /// 内容(专辑)评分 (0 ~ 5)
        /// </summary>
        public float Rating { get; set; }
        /// <summary>
        /// 主播(可能有多个)
        /// </summary>
        public string Podcasters { get; set; }
        /// <summary>
        /// 所属分类Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 内容(专辑)分类("free" -> 免费, "channel-sale" -> 付费, "program-sale" -> 付费 )
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 内容(专辑)的属性
        /// </summary>
        public List<AttributesItem> Attributes { get; set; }
    }
}
