using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Network
{
    [JsonObject(MemberSerialization.OptIn)]
    public class IPAddrItem
    {
        /// <summary>
        /// 城市
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "city", Required = Required.Default)]
        public string City { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ip", Required = Required.Default)]
        public string IPAddr { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "region", Required = Required.Default)]
        public string Region { get; set; }
    }
}
