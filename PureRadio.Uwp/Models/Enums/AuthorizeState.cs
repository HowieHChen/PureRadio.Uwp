using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Enums
{
    /// <summary>
    /// <see cref="AuthorizeState"/>表明当前的授权状态.
    /// </summary>
    public enum AuthorizeState
    {
        /// <summary>
        /// 用户已退出.
        /// </summary>
        SignedOut,

        /// <summary>
        /// 用户已登录.
        /// </summary>
        SignedIn,
    }
}
