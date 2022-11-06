using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace PureRadio.Uwp.Services.Interfaces
{
    public interface INavigateService
    {
        /// <summary>
        /// 导航事件
        /// </summary>
        public event EventHandler<AppNavigationEventArgs> Navigating;

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
        /// <param name="parameter">导航参数.</param>
        void NavigateToMainView(PageIds pageId, object parameter = null);

        /// <summary>
        /// 导航到指定的二级页面，传入的 PageIds 应该是二级页面的页面 Id.
        /// </summary>
        /// <param name="pageId">页面 Id.</param>
        /// <param name="parameter">导航参数.</param>
        void NavigateToSecondaryView(PageIds pageId, object parameter = null);

        /// <summary>
        /// 导航到播放页，传入播放参数.
        /// </summary>
        /// <param name="parameter">播放参数.</param>
        void NavigateToPlayView(PlaySnapshot parameter);
    }
}
