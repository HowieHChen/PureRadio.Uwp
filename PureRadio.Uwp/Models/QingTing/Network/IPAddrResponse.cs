using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Network
{
    /// <summary>
    /// IP地址响应
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class IPAddrResponse
    {
        /// <summary>
        /// 状态码(0成功)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "code", Required = Required.Default)]
        public int Code { get; set; }
        /// <summary>
        /// 消息("success"成功)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "msg", Required = Required.Default)]
        public string Msg { get; set; }
        /// <summary>
        /// IP地址响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data", Required = Required.Default)]
        public IPAddrItem Data { get; set; }
    }
}
