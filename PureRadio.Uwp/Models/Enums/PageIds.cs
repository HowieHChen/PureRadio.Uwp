using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Enums
{
    /// <summary>
    /// 页面标识.
    /// </summary>
    public enum PageIds
    {
        /// <summary>
        /// 未指定页面.
        /// </summary>
        None = -1,

        /// 一级页面[0,100)
        Main = 0,

        /// <summary>
        /// 主页
        /// </summary>
        Home = 1,

        /// <summary>
        /// 电台页
        /// </summary>
        Radio = 2,

        /// <summary>
        /// 有声内容页
        /// </summary>
        Content = 3,

        /// <summary>
        /// 库页
        /// </summary>
        Library = 4,

        /// <summary>
        /// 设置页
        /// </summary>
        Settings = 5,

        /// 二级页面[100,1000)

        /// <summary>
        /// 用户页(仅预留)
        /// </summary>
        User = 100,

        /// <summary>
        /// 搜索结果页
        /// </summary>
        Search = 101,

        /// <summary>
        /// 电台详细信息页
        /// </summary>
        RadioDetail = 102,

        /// <summary>
        /// 内容详细信息页
        /// </summary>
        ContentDetail = 103,

        /// <summary>
        /// 电台分类结果页
        /// </summary>
        RadioCategory = 104,

        /// <summary>
        /// 内容分类结果页
        /// </summary>
        ContentCategory = 105,

        /// <summary>
        /// 全屏的播放页
        /// </summary>
        Player = 1000,
    }
}
