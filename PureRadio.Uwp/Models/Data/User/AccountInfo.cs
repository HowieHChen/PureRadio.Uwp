using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace PureRadio.Uwp.Models.Data.User
{
    public class AccountInfo
    {
        /// <summary>
        /// 完整构造(在线账号)
        /// </summary>
        public AccountInfo(
            string qingtingId, string nickName, string userName, string createTime, string birthday, 
            string gender, string location, string signature, string avatar, string isBlocked, string phoneNumber)
        {
            IsOnline = true;
            PhoneNumber = phoneNumber;
            QingtingId = qingtingId;
            NickName = nickName;
            UserName = userName;
            CreateTime = createTime;
            Birthday = birthday;
            Gender = gender;
            Location = location;
            Signature = signature;
            Avatar = new Uri(avatar);
            IsBlocked = isBlocked;
        }

        /// <summary>
        /// 部分构造(离线账号)
        /// </summary>
        public AccountInfo()
        {
            var resourceLoader = new ResourceLoader();
            IsOnline = false;
            PhoneNumber = resourceLoader.GetString("LangLocalAccountPhone");
            QingtingId = string.Empty;
            NickName = UserName = resourceLoader.GetString("LangLocalAccountName");
            CreateTime = string.Empty;
            Birthday = string.Empty;
            Gender = "u";
            Location = string.Empty;
            Signature = resourceLoader.GetString("LangLocalAccountSignature");
            Avatar = new Uri("ms-appx:///Assets/Image/DefaultAvatar.png");
            IsBlocked = "0";
        }

        /// <summary>
        /// 是否为蜻蜓账户(在线)
        /// </summary>
        public bool IsOnline { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string QingtingId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 账号创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public Uri Avatar { get; set; }
        /// <summary>
        /// 帐号状态(是否被封禁)
        /// </summary>
        public string IsBlocked { get; set; }
    }
}
