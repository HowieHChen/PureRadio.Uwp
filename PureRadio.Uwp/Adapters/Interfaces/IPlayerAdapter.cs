using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;

namespace PureRadio.Uwp.Adapters.Interfaces
{
    /// <summary>
    /// 播放器相关数据适配器
    /// </summary>
    public interface IPlayerAdapter
    {
        /// <summary>
        /// 将播放项快照 <see cref="PlayItemSnapshot"/> 转换为媒体播放项 <see cref="MediaPlaybackItem"/>.
        /// </summary>
        /// <param name="item">播放项快照</param>
        /// <returns><see cref="MediaPlaybackItem"/></returns>
        MediaPlaybackItem ConvertToMediaPlaybackItem(PlayItemSnapshot item);
        /// <summary>
        /// 将电台详细信息 <see cref="RadioInfoDetail"/> 转换为播放项快照 <see cref="PlayItemSnapshot"/> (用于电台直播).
        /// </summary>
        /// <param name="item">电台详细信息</param>
        /// <returns><see cref="PlayItemSnapshot"/></returns>
        PlayItemSnapshot ConvertToPlayItemSnapshot(RadioInfoDetail item);
        /// <summary>
        /// 将电台详细信息 <see cref="RadioPlaylistDetail"/> 和节目列表 <see cref="RadioPlaylistDetail"/> 转换为播放项快照 <see cref="PlayItemSnapshot"/>.
        /// </summary>
        /// <param name="item">播放项快照</param>
        /// <returns> <see cref="PlayItemSnapshot"/></returns>
        List<PlayItemSnapshot> ConvertToPlayItemSnapshotList(RadioInfoDetail detail, List<RadioPlaylistDetail> playlist);
        /// <summary>
        /// 将内容详细信息 <see cref="ContentInfoDetail"/> 和播放列表 <see cref="ContentPlaylistDetail"/> 转换为播放项快照 <see cref="PlayItemSnapshot"/>.
        /// </summary>
        /// <param name="item">播放项快照</param>
        /// <returns><see cref="PlayItemSnapshot"/></returns>
        List<PlayItemSnapshot> ConvertToPlayItemSnapshotList(ContentInfoDetail detail, List<ContentPlaylistDetail> playlist);
    }
}
