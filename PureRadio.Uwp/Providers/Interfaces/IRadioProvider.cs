using PureRadio.Uwp.Models.Data.Radio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Providers.Interfaces
{
    /// <summary>
    /// 电台数据支持
    /// </summary>
    public interface IRadioProvider
    {
        /// <summary>
        /// 获取指定电台的详细信息.
        /// </summary>
        /// <param name="radioId">需获取的电台ID.</param>
        /// <param name="cancellationToken">异步中止令牌.</param>
        /// <returns>电台详细信息</returns>
        Task<RadioInfoDetail> GetRadioDetailInfo(int radioId, CancellationToken cancellationToken);

        /// <summary>
        /// 获取指定电台的前台、昨天、今天、明天的播放列表.
        /// </summary>
        /// <param name="radioId">需获取的电台ID.</param>
        /// <param name="cancellationToken">异步中止令牌.</param>
        /// <returns>电台播放列表集合</returns>
        Task<RadioPlaylistDetailSet> GetRadioPlaylistDetail(int radioId, CancellationToken cancellationToken);
    }
}
