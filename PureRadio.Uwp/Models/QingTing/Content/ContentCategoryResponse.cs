using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Content
{
    /// <summary>
    /// 专辑分类和属性请求响应
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ContentCategoryResponse
    {
        /// <summary>
        /// 结果总数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "total", Required = Required.Default)]
        public int Total { get; set; }
        /// <summary>
        /// 专辑分类和属性请求响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data", Required = Required.Default)]
        public ContentCategoryData Data { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "errormsg", Required = Required.Default)]
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "errorno", Required = Required.Default)]
        public int ErrorNo { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ContentCategoryData
    {
        /// <summary>
        /// 分类的内容结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "channels", Required = Required.Default)]
        public List<ContentCategoryItem> Contents { get; set; }
        /// <summary>
        /// 当前分类Id下的可用属性
        /// </summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "filters", Required = Required.Default)]
        //public List<FiltersItem> Filters { get; set; }
    }
}
