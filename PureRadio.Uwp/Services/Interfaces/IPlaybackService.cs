using PureRadio.Uwp.Models.Args;
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

namespace PureRadio.Uwp.Services.Interfaces
{
    public interface IPlaybackService
    {
        /// <summary>
        /// 播放器
        /// </summary>
        MediaPlayer AudioPlayer { get; }

        /// <summary>
        /// 播放器状态
        /// </summary>
        MediaPlaybackState AudioPlaybackState { get; }

        /// <summary>
        /// 当前播放位置
        /// </summary>
        TimeSpan NowPosition { get; }

        /// <summary>
        /// 当前总时长
        /// </summary>
        TimeSpan NaturalDuration { get; }

        /// <summary>
        /// 播放器状态改变事件
        /// </summary>
        event EventHandler<PlayerStateChangedEventArgs> PlayerStateChanged;

        /// <summary>
        /// 播放项更改事件
        /// </summary>
        event EventHandler<PlayerItemChangedEventArgs> PlayerItemChanged;

        void PlayRadioLive(int radioId);

        void PlayRadioLive(int radioId, PlayItemSnapshot playItem);

        void PlayRadioDemand(int radioId, int index, PlaylistDay day);

        void PlayRadioDemand(int radioId, int index, List<PlayItemSnapshot> radioPlaylist);

        void PlayContent(int contentId, int programId, string version);

        void PlayContent(int contentId, int programId, List<PlayItemSnapshot> contentPlaylist);

        PlayStateSnapshot GetCurrentPlayerState();

        PlayItemSnapshot GetCurrentPlayItem();

        List<PlayItemSnapshot> GetCurrentPlayList();

        void Play();

        void Pause();

        void Previous();

        void Next();

        void SetMute(bool muted);

        void SetVolume(double volume);

        void SetPosition(int position);

        void Refresh();
    }
}
