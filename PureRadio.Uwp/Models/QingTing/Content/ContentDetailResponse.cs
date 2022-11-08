using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Content
{
    /// <summary>
    /// 内容(专辑)详细信息响应
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ContentDetailResponse
    {
        /// <summary>
        /// 内容(专辑)详细信息响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "channel", Required = Required.Default)]
        public ContentDetailItem Data { get; set; }
        /// <summary>
        /// 内容(专辑)的属性
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "attributes", Required = Required.Default)]
        public List<AttributesItem> Attributes { get; set; }
        /// <summary>
        /// 推荐的内容(专辑)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "recommends", Required = Required.Default)]
        public List<RecommendedContent> RecommendedContents { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class AttributesItem
    {
        /// <summary>
        /// 属性名
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string Name { get; set; }
        /// <summary>
        /// 属性Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public int AttrId { get; set; }
        /// <summary>
        /// 属性所属分类Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "categoryId", Required = Required.Default)]
        public int CategoryId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class RecommendedContent
    {
        /// <summary>
        /// 内容(专辑)Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "channelId", Required = Required.Default)]
        public int ContentId { get; set; }
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
        /// 内容(专辑)封面图片(URL)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "img", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 内容(专辑)评分 (0 ~ 10)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "score", Required = Required.Default)]
        public int Rating { get; set; }
        /// <summary>
        /// 内容(专辑)播放量
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "playCount", Required = Required.Default)]
        public string PlayCount { get; set; }
        /// <summary>
        /// 内容(专辑)分类(0 -> 免费, 1 -> 付费(小说), 2 -> 付费(专辑) )
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "itemType", Required = Required.Default)]
        public int ContentType { get; set; }
        /// <summary>
        /// 内容(专辑)所包含的节目个数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "programCount", Required = Required.Default)]
        public int ProgramCount { get; set; }
    }

}
