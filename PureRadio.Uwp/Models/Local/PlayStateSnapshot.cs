using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;

namespace PureRadio.Uwp.Models.Local
{
    public class PlayStateSnapshot
    {
        public PlayStateSnapshot(
            MediaPlaybackState playerState, bool canPrevious, bool canNext, 
            bool isMuted, double volume, int totalSeconds, int nowPosition)
        {
            PlayerState = playerState;
            CanPrevious = canPrevious;
            CanNext = canNext;
            IsMuted = isMuted;
            Volume = volume;
            TotalSeconds = totalSeconds;
            NowPosition = nowPosition;
        }

        public MediaPlaybackState PlayerState { get; }
        public bool CanPrevious { get; }
        public bool CanNext { get; }
        public bool IsMuted { get; }
        public double Volume { get; }
        public int TotalSeconds { get; }
        public int NowPosition { get; }
    }
}
