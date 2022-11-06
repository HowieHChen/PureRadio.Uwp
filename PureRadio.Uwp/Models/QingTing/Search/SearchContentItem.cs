using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace PureRadio.Uwp.Models.QingTing.Search
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SearchContentItem
    {
        /// <summary>
        /// 专辑ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public int ContentId { get; set; }
        /// <summary>
        /// 专辑标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 搜索结果类型(专辑(有声内容)应为"channel_ondemand")
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Required.Default)]
        public string Type { get; set; }
        /// <summary>
        /// 主播(专辑所有者)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "podcaster", Required = Required.Default)]
        public string Podcaster { get; set; }
        /// <summary>
        /// 专辑封面图片(URL)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "cover", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 专辑简介
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string Description { get; set; }
        /// <summary>
        /// 专辑播放量
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "playcount", Required = Required.Default)]
        public string PlayCount { get; set; }
        /// <summary>
        /// 专辑内所含节目数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "program_count", Required = Required.Default)]
        public int ProgramCount { get; set; }
    }
}
