using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Database
{
    [Table("AccountSnapshots")]
    public class AccountSnapshot : DbObject, IEquatable<AccountSnapshot>
    {
        /// <summary>
        /// 是否为蜻蜓账户(在线)
        /// </summary>
        [Column(nameof(IsOnline))]
        public bool IsOnline { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Column(nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Column(nameof(QingtingId))]
        public string QingtingId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Column(nameof(NickName))]
        public string NickName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Column(nameof(UserName))]
        public string UserName { get; set; }
        /// <summary>
        /// 账号创建时间
        /// </summary>
        [Column(nameof(CreateTime))]
        public string CreateTime { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [Column(nameof(Birthday))]
        public string Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Column(nameof(Gender))]
        public string Gender { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        [Column(nameof(Location))]
        public string Location { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [Column(nameof(Signature))]
        public string Signature { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Column(nameof(Avatar))]
        public string Avatar { get; set; }
        /// <summary>
        /// 帐号状态(是否被封禁)
        /// </summary>
        [Column(nameof(IsBlocked))]
        public string IsBlocked { get; set; }

        public AccountSnapshot() { }

        public AccountSnapshot(
            int mainId, bool isOnline, string phoneNumber, string qingtingId, string nickName, string userName, string createTime, 
            string birthday, string gender, string location, string signature, string avatar, string isBlocked)
        {
            MainId = mainId;
            IsOnline = isOnline;
            PhoneNumber = phoneNumber;
            QingtingId = qingtingId;
            NickName = nickName;
            UserName = userName;
            CreateTime = createTime;
            Birthday = birthday;
            Gender = gender;
            Location = location;
            Signature = signature;
            Avatar = avatar;
            IsBlocked = isBlocked;
        }



        /// <summary>
        /// 标题
        /// </summary>
        public override string ToString()
        {
            return NickName;
        }

        public bool Equals(AccountSnapshot other)
        {
            return MainId == other.MainId && QingtingId == other.QingtingId;
        }

        public override int GetHashCode()
        {
            return MainId.GetHashCode() + QingtingId.GetHashCode();
        }
    }
}
