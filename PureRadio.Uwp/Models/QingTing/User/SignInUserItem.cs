using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.User
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SignInUserItem
    {
        /// <summary>
        /// 用户Id.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "qingting_id ", Required = Required.Default)]
        public string QingtingId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "nick_name ", Required = Required.Default)]
        public string NickName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userName ", Required = Required.Default)]
        public string UserName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "phone_number ", Required = Required.Default)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 手机号地区
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "area_code ", Required = Required.Default)]
        public string AreaCode { get; set; }
        /// <summary>
        /// 是否是新用户
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "newbie ", Required = Required.Default)]
        public bool Newbie { get; set; }
        /// <summary>
        /// 工作
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "job ", Required = Required.Default)]
        public string Job { get; set; }
        /// <summary>
        /// 账号创建时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "create_time ", Required = Required.Default)]
        public string CreateTime { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "birthday ", Required = Required.Default)]
        public string Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "gender ", Required = Required.Default)]
        public string Gender { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "location ", Required = Required.Default)]
        public string Location { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "signature ", Required = Required.Default)]
        public string Signature { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "avatar ", Required = Required.Default)]
        public string Avatar { get; set; }
        /// <summary>
        /// 访问令牌.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "access_token ", Required = Required.Default)]
        public string AccessToken { get; set; }
        /// <summary>
        /// 刷新令牌.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "refresh_token ", Required = Required.Default)]
        public string RefreshToken { get; set; }
        /// <summary>
        /// 过期时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "expires_in ", Required = Required.Default)]
        public int ExpiresIn { get; set; }
        /// <summary>
        /// 帐号状态(是否被封禁)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "is_blocked ", Required = Required.Default)]
        public string IsBlocked { get; set; }
    }
}
