using PureRadio.Uwp.Models.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;

namespace PureRadio.Uwp.Models.Args
{
    public class PlayerStateChangedEventArgs : EventArgs
    {
        public PlayerStateChangedEventArgs(PlayStateSnapshot snapshot)
        {
            Snapshot = snapshot;
        }

        public PlayStateSnapshot Snapshot { get; }
    }
}
