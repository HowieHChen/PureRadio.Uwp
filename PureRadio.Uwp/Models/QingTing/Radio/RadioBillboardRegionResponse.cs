using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Radio
{
    /// <summary>
    /// 电台地区排行榜响应
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RadioBillboardRegionResponse
    {

        /// <summary>
        /// 电台地区排行榜成功标志
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Success", Required = Required.Default)]
        public string Success { get; set; }
        /// <summary>
        /// 电台地区排行榜响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Data", Required = Required.Default)]
        public List<RadioBillboardItem> Data { get; set; }
    }
}
