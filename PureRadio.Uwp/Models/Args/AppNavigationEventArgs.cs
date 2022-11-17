using PureRadio.Uwp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace PureRadio.Uwp.Models.Args
{
    public sealed class AppNavigationEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppNavigationEventArgs"/> class.
        /// </summary>
        /// <param name="type">导航类型.</param>
        /// <param name="pageId">导航页面 Id.</param>
        /// <param name="parameter">导航参数.</param>
        public AppNavigationEventArgs(
            NavigationType type,
            PageIds pageId,
            object parameter)
        {
            Type = type;
            PageId = pageId;
            Parameter = parameter;
            TransitionInfo = new EntranceNavigationTransitionInfo();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AppNavigationEventArgs"/> class.
        /// </summary>
        /// <param name="type">导航类型.</param>
        /// <param name="pageId">导航页面 Id.</param>
        /// <param name="parameter">导航参数.</param>\
        /// <param name="transitionInfo">导航动画.</param>
        public AppNavigationEventArgs(
            NavigationType type,
            PageIds pageId,
            object parameter,
            NavigationTransitionInfo transitionInfo)
        {
            Type = type;
            PageId = pageId;
            Parameter = parameter;
            TransitionInfo = transitionInfo;
        }

        /// <summary>
        /// 导航类型.
        /// </summary>
        public NavigationType Type { get; }

        /// <summary>
        /// 页面 Id.
        /// </summary>
        public PageIds PageId { get; }

        /// <summary>
        /// 导航动画.
        /// </summary>
        public NavigationTransitionInfo TransitionInfo { get; }

        /// <summary>
        /// 导航参数.
        /// </summary>
        public object Parameter { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is AppNavigationEventArgs args && PageId == args.PageId && EqualityComparer<object>.Default.Equals(Parameter, args.Parameter);

        /// <inheritdoc/>
        public override int GetHashCode()
            => PageId.GetHashCode() + Parameter?.GetHashCode() ?? 0;
    }
}
