using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Content
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ContentDetailItem
    {
        /// <summary>
        /// 内容(专辑)封面图片(URL)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "img", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 内容(专辑)Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "channelId", Required = Required.Default)]
        public int ContentId { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "v", Required = Required.Default)]
        public string Version { get; set; }
        /// <summary>
        /// 内容(专辑)标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 内容(专辑)简介
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "desc", Required = Required.Default)]
        public string Description { get; set; }
        /// <summary>
        /// 内容(专辑)所包含的节目个数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "programCount", Required = Required.Default)]
        public int ProgramCount { get; set; }
        /// <summary>
        /// 内容(专辑)播放量
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "playCount", Required = Required.Default)]
        public string PlayCount { get; set; }
        /// <summary>
        /// 内容(专辑)评分 (0 ~ 10)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "score", Required = Required.Default)]
        public int Rating { get; set; }
        /// <summary>
        /// 主播(可能有多个)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "podcasters", Required = Required.Default)]
        public List<PodcasterItem> Podcasters { get; set; }
        /// <summary>
        /// 所属分类Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "categoryId", Required = Required.Default)]
        public int CategoryId { get; set; }
        /// <summary>
        /// 内容(专辑)分类("free" -> 免费, "channel-sale" -> 付费, "program-sale" -> 付费 )
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "channelType", Required = Required.Default)]
        public string ContentType { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class PodcasterItem
    {
        /// <summary>
        /// 主播蜻蜓ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "qingtingId", Required = Required.Default)]
        public string QingtingID { get; set; }
        /// <summary>
        /// 主播用户名
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string Name { get; set; }
        /// <summary>
        /// 主播头像
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "avatar", Required = Required.Default)]
        public string Avatar { get; set; }
    }
}
