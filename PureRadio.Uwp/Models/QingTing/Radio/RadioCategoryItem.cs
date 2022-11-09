using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Radio
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RadioCategoryItem
    {
        /// <summary>
        /// 电台ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "content_id", Required = Required.Default)]
        public int RadioId { get; set; }
        /// <summary>
        /// 电台类型 (应为"channel")
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "content_type", Required = Required.Default)]
        public string ContentType { get; set; }
        /// <summary>
        /// 电台封面图片(URL)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "cover", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 电台标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 电台简介
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string Description { get; set; }
        /// <summary>
        /// 正在播放的节目
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "nowplaying", Required = Required.Default)]
        public NowplayingProgram Nowplaying { get; set; }
        /// <summary>
        /// 电台听众计数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "audience_count", Required = Required.Default)]
        public int AudienceCount { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "update_time", Required = Required.Default)]
        public string UpdateTime { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "categories", Required = Required.Default)]
        public List<CategoriesItem> Categories { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CategoriesItem
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public int CategoryId { get; set; }
        /// <summary>
        /// 分类标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
    }

}
