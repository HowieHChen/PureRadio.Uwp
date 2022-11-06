using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Search
{
    /// <summary>
    /// 电台搜索响应
    /// </summary>
    public class SearchRadioResponse
    {
        /// <summary>
        /// 电台搜索响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data", Required = Required.Default)]
        public SearchRadioData Data { get; set; }
    }
    public class SearchRadioData
    {
        /// <summary>
        /// 电台搜索结果数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "searchResultsPage", Required = Required.Default)]
        public SearchRadioResult SearchRadioResults { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class SearchRadioResult
    {
        /// <summary>
        /// 结果计数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "numFound", Required = Required.Default)]
        public int ResultCount { get; set; }
        /// <summary>
        /// 电台搜索结果项列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "searchData", Required = Required.Default)]
        public List<SearchRadioItem> SearchDatas { get; set; }
    }

}
