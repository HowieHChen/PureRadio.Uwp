using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Enums
{
    public enum MediaPlayType
    {
        /// <summary>
        /// 空
        /// </summary>
        None,
        /// <summary>
        /// 电台直播
        /// </summary>
        RadioLive,
        /// <summary>
        /// 电台回放
        /// </summary>
        RadioDemand,
        /// <summary>
        /// 内容(专辑)播放
        /// </summary>
        ContentDemand,
    }
}
