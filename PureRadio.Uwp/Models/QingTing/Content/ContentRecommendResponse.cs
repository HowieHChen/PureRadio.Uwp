using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Content
{
    /// <summary>
    /// 内容分类推荐响应
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ContentRecommendResponse
    {
        /// <summary>
        /// 内容分类推荐响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data", Required = Required.Default)]
        public ContentRecommendResponseData Data { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ContentRecommendResponseData
    {
        /// <summary>
        /// 内容分类推荐数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "homePage", Required = Required.Default)]
        public ContentHomePage Contents { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ContentHomePage
    {
        /// <summary>
        /// 分类列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "categories", Required = Required.Default)]
        public List<ContentCategoriesItem> Categories { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ContentCategoriesItem
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "categoryId", Required = Required.Default)]
        public int CategoryId { get; set; }
        /// <summary>
        /// 分类标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string CategoryTittle { get; set; }
        /// <summary>
        /// 所属的推荐内容
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data", Required = Required.Default)]
        public List<ContentRecommendItem> CategoryItems { get; set; }
    }
}
