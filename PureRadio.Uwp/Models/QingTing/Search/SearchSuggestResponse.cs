using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Search
{
    /// <summary>
    /// 搜索建议响应
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SearchSuggestResponse
    {
        /// <summary>
        /// 结果计数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "total", Required = Required.Default)]
        public int ResultCount { get; set; }
        /// <summary>
        /// 搜索建议项列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data", Required = Required.Default)]
        public List<SearchSuggestItem> Data { get; set; }
    }
}
