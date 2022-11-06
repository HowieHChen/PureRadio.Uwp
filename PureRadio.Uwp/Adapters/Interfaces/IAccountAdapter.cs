using PureRadio.Uwp.Models.Data.User;
using PureRadio.Uwp.Models.QingTing.Search;
using PureRadio.Uwp.Models.QingTing.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Adapters.Interfaces
{
    /// <summary>
    /// 账号数据适配器接口定义.
    /// </summary>
    public interface IAccountAdapter
    {
        /// <summary>
        /// 将来自 Web 的登录结果 <see cref="SignInUserItem"/> 和手机号 <see cref="string"/> 转换为本地账号信息 <see cref="AccountInfo"/> 和本地Token信息 <see cref="TokenInfo"/>.
        /// </summary>
        /// <param name="item">来自 Web 的搜索建议条目.</param>
        (AccountInfo, TokenInfo) ConvertToAccountInfo(SignInUserItem item, string phoneNumber);
    }
}
