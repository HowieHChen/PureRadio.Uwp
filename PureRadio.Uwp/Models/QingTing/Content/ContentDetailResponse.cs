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
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "channelId", Required = Required.Default)]
        public int ContentId { get; set; }
        /// <summary>
        /// 张召忠谈天下
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 局座最近在干嘛？张召忠特辑，三言两语，幽默犀利，带您领略军事风采。搜索微信公众号：复兴路7号 ，看局座更多图文内容
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "desc", Required = Required.Default)]
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "img", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "score", Required = Required.Default)]
        public int Rating { get; set; }
        /// <summary>
        /// 2.1亿
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "playCount", Required = Required.Default)]
        public string PlayCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "itemType", Required = Required.Default)]
        public int ContentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "programCount", Required = Required.Default)]
        public int ProgramCount { get; set; }
    }

}
