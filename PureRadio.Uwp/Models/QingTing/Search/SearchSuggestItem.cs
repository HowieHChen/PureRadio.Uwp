using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Search
{
    /// <summary>
    /// 搜索建议项目
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SearchSuggestItem
    {
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
    }
}
