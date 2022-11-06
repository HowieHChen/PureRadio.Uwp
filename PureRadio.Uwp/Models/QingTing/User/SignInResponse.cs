using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.User
{
    /// <summary>
    /// 登录响应
    /// </summary>
    public class SignInResponse
    {
        /// <summary>
        /// 错误号
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "errorno ", Required = Required.Default)]
        public int ErrorNo { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "errormsg ", Required = Required.Default)]
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 登录响应结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data ", Required = Required.Default)]
        public SignInUserItem Data { get; set; }
    }
}
