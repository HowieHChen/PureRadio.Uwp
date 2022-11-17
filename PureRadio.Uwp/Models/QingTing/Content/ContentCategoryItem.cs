using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Content
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ContentCategoryItem
    {
        /// <summary>
        /// 内容(专辑)Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public int ContentId { get; set; }
        /// <summary>
        /// 内容(专辑)标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 内容(专辑)简介
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string Description { get; set; }
        /// <summary>
        /// 内容(专辑)封面图片(URL)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "cover", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 搜索结果类型(专辑(有声内容)应为"channel_ondemand")
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Required.Default)]
        public string Type { get; set; }
        /// <summary>
        /// 内容(专辑)评分 (0 ~ 10)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "score", Required = Required.Default)]
        public int Rating { get; set; }
        /// <summary>
        /// 内容(专辑)播放量
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "playcount", Required = Required.Default)]
        public string PlayCount { get; set; }
    }
}
