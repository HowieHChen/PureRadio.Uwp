using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.QingTing.Radio;
using PureRadio.Uwp.Models.QingTing.Recommend;
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

        /// <summary>
        /// 将来自 Web 的电台分类结果项 <see cref="RadioCategoryItem"/> 转换为本地电台分类结果项 <see cref="RadioInfoSummary"/> .
        /// </summary>
        /// <param name="item">来自 Web 的电台分类结果项.</param>
        /// <returns><see cref="RadioInfoSummary"/>.</returns>
        RadioInfoSummary ConvertToRadioInfoSummary(RadioCategoryItem item);

        /// <summary>
        /// 将来自 Web 的电台推荐项 <see cref="RadioRecommendItem"/> 转换为本地电台推荐项 <see cref="RadioInfoRecommend"/> .
        /// </summary>
        /// <param name="item">来自 Web 的电台推荐项</param>
        /// <returns><see cref="RadioInfoRecommend"/>.</returns>
        RadioInfoRecommend ConvertToRadioInfoRecommend(RadioRecommendItem item);

        /// <summary>
        /// 将来自 Web 的电台直播推荐项 <see cref="RecommendRadioLiveItem"/> 转换为本地电台详情条目 <see cref="RadioInfoDetail"/> .
        /// </summary>
        /// <param name="item">来自 Web 的电台直播推荐项</param>
        /// <returns><see cref="RadioInfoDetail"/>.</returns>
        RadioInfoDetail ConvertToRadioInfoDetail(RecommendRadioLiveItem item);

        /// <summary>
        /// 将来自 Web 的电台回放推荐项 <see cref="RecommendRadioReplayItem"/> 转换为本地电台节目推荐项 <see cref="RadioReplayInfo"/> .
        /// </summary>
        /// <param name="item">来自 Web 的电台回放推荐项</param>
        /// <returns><see cref="RadioReplayInfo"/>.</returns>
        RadioReplayInfo ConvertToRadioReplayInfo(RecommendRadioReplayItem item);
    }
}
