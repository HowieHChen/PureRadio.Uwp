using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Content
{
    /// <summary>
    /// 内容(专辑)节目列表响应
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ContentProgramListResponse
    {
        /// <summary>
        /// 内容(专辑)节目列表响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data", Required = Required.Default)]
        public ProgramListData Data { get; set; }
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
    public class ProgramListData
    {
        /// <summary>
        /// 版本号
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "version", Required = Required.Default)]
        public string Version { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "total", Required = Required.Default)]
        public int Total { get; set; }
        /// <summary>
        /// 当前页号
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "curpage", Required = Required.Default)]
        public int CurPage { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "pagesize", Required = Required.Default)]
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页号所含节目列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "programs", Required = Required.Default)]
        public List<ContentProgramListItem> Programs { get; set; }
    }

}
