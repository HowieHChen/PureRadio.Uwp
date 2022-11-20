using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Content
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ContentRecommendItem
    {
        /// <summary>
        /// 内容(专辑)标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 推荐词
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "recWords", Required = Required.Default)]
        public string RecWords { get; set; }
        /// <summary>
        /// 内容(专辑)封面图片(URL)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "imgUrl", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 内容(专辑)播放量
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "playCnt", Required = Required.Default)]
        public string PlayCount { get; set; }
        /// <summary>
        /// 内容(专辑)评分 (0 ~ 10)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "score", Required = Required.Default)]
        public int Score { get; set; }
        /// <summary>
        /// 内容(专辑)所包含的节目个数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "programCnt", Required = Required.Default)]
        public int ProgramCount { get; set; }
        /// <summary>
        /// 内容(专辑)跳转信息
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "link", Required = Required.Default)]
        public ContentLink Link { get; set; }
        /// <summary>
        /// 是否免费
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "free", Required = Required.Default)]
        public bool IsFree { get; set; }
        /// <summary>
        /// 所属分类标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "category", Required = Required.Default)]
        public string CategoryTitle { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ContentLink
    {
        /// <summary>
        /// 类型(Channel)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Required.Default)]
        public string Type { get; set; }
        /// <summary>
        /// 内容(专辑)Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "content", Required = Required.Default)]
        public string ContentId { get; set; }
    }
}
