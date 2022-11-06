using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.User
{
    /// <summary>
    /// 令牌信息.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class TokenInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="qingtingId">蜻蜓ID</param>
        /// <param name="accessToken">访问Token</param>
        /// <param name="refreshToken">刷新Token</param>
        /// <param name="expiresIn">有效期</param>
        public TokenInfo(string qingtingId, string accessToken, string refreshToken, int expiresIn)
        {
            QingtingId = qingtingId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
        }

        /// <summary>
        /// 用户Id.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "qingting_id ", Required = Required.Default)]
        public string QingtingId { get; set; }

        /// <summary>
        /// 访问令牌.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "access_token", Required = Required.Default)]
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新令牌.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "refresh_token", Required = Required.Default)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 过期时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "expires_in", Required = Required.Default)]
        public int ExpiresIn { get; set; }
    }
}
