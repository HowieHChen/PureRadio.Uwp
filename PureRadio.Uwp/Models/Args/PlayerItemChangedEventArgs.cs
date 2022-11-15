using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls;

namespace PureRadio.Uwp.Models.Args
{
    public sealed class PlayerItemChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerItemChangedEventArgs"/> class.
        /// </summary>
        /// <param name="snapshot">播放快照</param>
        public PlayerItemChangedEventArgs(PlayItemSnapshot snapshot)
        {
            Snapshot = snapshot;
        }

        /// <summary>
        /// 播放项快照.
        /// </summary>
        public PlayItemSnapshot Snapshot { get; }

        /// <summary>
        /// 携带参数.
        /// </summary>
        public object Parameter { get; }

        public override bool Equals(object obj) => obj is PlayerItemChangedEventArgs args && Snapshot.MainId == args.Snapshot.MainId && Snapshot.SecondaryId == args.Snapshot.SecondaryId &&  EqualityComparer<object>.Default.Equals(Parameter, args.Parameter);

        /// <inheritdoc/>
        public override int GetHashCode()
            => Snapshot.MainId.GetHashCode() + Snapshot.SecondaryId.GetHashCode() + Parameter?.GetHashCode() ?? 0;
    }
}
