using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Animation;

namespace PureRadio.Uwp.Services.Interfaces
{
    public interface INavigateService
    {
        /// <summary>
        /// 导航事件
        /// </summary>
        event EventHandler<AppNavigationEventArgs> Navigating;

        /// <summary>
        /// 当前主视图展示的页面 Id.
        /// </summary>
        PageIds MainViewId { get; }

        /// <summary>
        /// 当前二级页面展示的页面 Id.
        /// </summary>
        PageIds SecondaryViewId { get; }

        /// <summary>
        /// 在主视图中进行导航，传入的 PageIds 应该是主视图的页面 Id.
        /// </summary>
        /// <param name="pageId">页面 Id.</param>
        /// <param name="transitionInfo">导航动画.</param>
        /// <param name="parameter">导航参数.</param>
        void NavigateToMainView(PageIds pageId, NavigationTransitionInfo transitionInfo, object parameter = null);

        /// <summary>
        /// 导航到指定的二级页面，传入的 PageIds 应该是二级页面的页面 Id.
        /// </summary>
        /// <param name="pageId">页面 Id.</param>
        /// <param name="transitionInfo">导航动画.</param>
        /// <param name="parameter">导航参数.</param>
        void NavigateToSecondaryView(PageIds pageId, NavigationTransitionInfo transitionInfo, object parameter = null);

        /// <summary>
        /// 导航到播放页.
        /// </summary>
        /// <param name="transitionInfo">导航动画.</param>
        /// <param name="back">返回主页面标志.</param>
        void NavigateToPlayView(NavigationTransitionInfo transitionInfo, bool back = false);
    }
}
