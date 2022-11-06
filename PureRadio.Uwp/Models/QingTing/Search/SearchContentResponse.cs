using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Search
{
    /// <summary>
    /// 有声内容(专辑)搜索响应
    /// </summary>
    public class SearchContentResponse
    {
        /// <summary>
        /// 有声内容(专辑)搜索响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data", Required = Required.Default)]
        public SearchContentData Data { get; set; }
    }

    public class SearchContentData
    {
        /// <summary>
        /// 有声内容(专辑)搜索结果数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "searchResultsPage", Required = Required.Default)]
        public SearchContentResult SearchContentResults { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class SearchContentResult
    {
        /// <summary>
        /// 结果计数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "numFound", Required = Required.Default)]
        public int ResultCount { get; set; }
        /// <summary>
        /// 有声内容(专辑)搜索结果项列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "searchData", Required = Required.Default)]
        public List<SearchContentItem> SearchDatas { get; set; }
    }
}
