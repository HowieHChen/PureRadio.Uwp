using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Radio
{
    /// <summary>
    /// 电台详细信息响应
    /// </summary>
    public class RadioDetailResponse
    {
        /// <summary>
        /// 电台详细信息成功标志
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Success", Required = Required.Default)]
        public string Success { get; set; }
        /// <summary>
        /// 电台详细信息响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Data", Required = Required.Default)]
        public RadioDetailItem Data { get; set; }
    }
}
