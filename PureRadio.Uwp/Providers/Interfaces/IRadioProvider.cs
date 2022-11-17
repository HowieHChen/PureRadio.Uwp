using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
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

        /// <summary>
        /// 获取指定电台的指定日期(前台、昨天、今天、明天)的播放列表.
        /// </summary>
        /// <param name="radioId">需获取的电台ID.</param>
        /// <param name="day">需获取的日期</param>
        /// <param name="cancellationToken">异步中止令牌.</param>
        /// <returns>电台播放列表集合</returns>
        Task<List<RadioPlaylistDetail>> GetRadioPlaylistDetail(int radioId, PlaylistDay day, CancellationToken cancellationToken);

        /// <summary>
        /// 获取指定分类的电台结果.
        /// </summary>
        /// <param name="categoryId">分类Id.</param>
        /// <param name="cancellationToken">异步中止令牌.</param>
        /// <param name="page">页数</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        Task<ResultSet<RadioInfoCategory>> GetRadioCategoryResult(int categoryId, CancellationToken cancellationToken, int page = 1, int pageSize = 30);
    }
}
