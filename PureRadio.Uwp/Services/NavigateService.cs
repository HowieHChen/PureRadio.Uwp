using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace PureRadio.Uwp.Services
{
    public sealed class NavigateService : INavigateService
    {
        public PageIds MainViewId { get; set; }
        public PageIds SecondaryViewId { get; set; }

        public event EventHandler<AppNavigationEventArgs> Navigating;

        public void NavigateToMainView(PageIds pageId, object parameter = null)
        {
            //if (pageId != MainViewId)
            //{
            //    MainViewId = pageId;
                
            //}
            var args = new AppNavigationEventArgs(NavigationType.Main, pageId, parameter);
            Navigating?.Invoke(this, args);
        }

        public void NavigateToSecondaryView(PageIds pageId, object parameter = null)
        {
            //if (pageId != SecondaryViewId)
            //{
            //    SecondaryViewId = pageId;
                
            //}
            var args = new AppNavigationEventArgs(NavigationType.Secondary, pageId, parameter);
            Navigating?.Invoke(this, args);
        }

        public void NavigateToPlayView(PlayItemSnapshot parameter)
        {
            var args = new AppNavigationEventArgs(NavigationType.Player, PageIds.Player, parameter);
            Navigating?.Invoke(this, args);
        }
    }
}
