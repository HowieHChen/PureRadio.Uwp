using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Enums
{
    /// <summary>
    /// Http请求类型
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// 默认方法
        /// </summary>
        Default,
        /// <summary>
        /// 登录
        /// </summary>
        Login,
        /// <summary>
        /// 认证(刷新Token)
        /// </summary>
        Auth,
        /// <summary>
        /// 搜索
        /// </summary>
        Search,
        /// <summary>
        /// 播放点播内容(专辑内的节目)
        /// </summary>
        PlayContent,
    }
}
