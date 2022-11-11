using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.QingTing.Radio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Adapters.Interfaces
{
    /// <summary>
    /// 电台数据适配器定义
    /// </summary>
    public interface IRadioAdapter
    {
        /// <summary>
        /// 将来自 Web 的电台详情条目 <see cref="RadioDetailItem"/> 转换为本地电台详情条目 <see cref="RadioInfoDetail"/> .
        /// </summary>
        /// <param name="item">来自 Web 的电台详情条目.</param>
        /// <returns><see cref="RadioInfoDetail"/>.</returns>
        RadioInfoDetail ConvertToRadioInfoDetail(RadioDetailItem item);

        /// <summary>
        /// 将来自 Web 的电台播放列表项 <see cref="RadioPlaylistItem"/> 转换为本地电台播放列表项 <see cref="RadioPlaylistDetail"/> .
        /// </summary>
        /// <param name="item">将来自 Web 的电台播放列表项.</param>
        /// <returns><see cref="RadioPlaylistDetail"/>.</returns>
        RadioPlaylistDetail ConvertToRadioPlaylistItem(RadioPlaylistItem item);
    }
}
