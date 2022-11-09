using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Radio
{
    /// <summary>
    /// 电台分类请求响应
    /// </summary>
    public class RadioCategoryResponse
    {
        /// <summary>
        /// 电台分类请求成功标志
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Success", Required = Required.Default)]
        public string Success { get; set; }
        /// <summary>
        /// 电台分类请求响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Data", Required = Required.Default)]
        public List<RadioCategoryItem> Data { get; set; }
    }
}
