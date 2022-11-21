using PureRadio.Uwp.Models.Data.User;
using PureRadio.Uwp.Models.Database;
using PureRadio.Uwp.Models.QingTing.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace PureRadio.Uwp.Adapters.Interfaces
{
    public class AccountAdapter : IAccountAdapter
    {
        public (AccountInfo, TokenInfo) ConvertToAccountInfo(SignInUserItem item, string phoneNumber)
        {
            AccountInfo accountInfo = new(
                item.QingtingId, item.NickName, item.NickName, item.CreateTime, item.Birthday, 
                item.Gender, item.Location, item.Signature, item.Avatar, item.IsBlocked, phoneNumber
                );
            TokenInfo tokenInfo = new(item.QingtingId, item.AccessToken, item.RefreshToken, item.ExpiresIn);

            return (accountInfo, tokenInfo);
        }

        public AccountInfo ConvertToAccountInfo(AccountSnapshot item)
            => new AccountInfo(
                item.QingtingId, item.NickName, item.UserName, item.CreateTime,
                item.Birthday, item.Gender, item.Location, item.Signature,
                item.Avatar, item.IsBlocked, item.PhoneNumber);

        public AccountSnapshot ConvertToAccountSnapshot(AccountInfo item)
            => new AccountSnapshot(0, item.IsOnline, item.PhoneNumber, item.QingtingId,
                item.NickName, item.UserName, item.CreateTime, item.Birthday, item.Gender,
                item.Location, item.Signature, item.Avatar.OriginalString, item.IsBlocked);
    }
}
