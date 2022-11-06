using CommunityToolkit.Mvvm.DependencyInjection;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Uwp.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RootPage : Page
    {
        public RootPage()
        {
            this.InitializeComponent();

            Loaded += RootPage_Loaded;
            Unloaded += RootPage_Unloaded;

            // Hide default title bar.
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            // Set caption buttons background to transparent.
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            RootFrame.Navigate(typeof(MainPage));
            
        }

        private void RootPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Ioc.Default.GetRequiredService<INavigateService>().Navigating -= Navigate_Navigating;
            
        }

        private void RootPage_Loaded(object sender, RoutedEventArgs e)
        {
            Ioc.Default.GetRequiredService<INavigateService>().Navigating += Navigate_Navigating;
        }


        private void Navigate_Navigating(object sender, Models.Args.AppNavigationEventArgs e)
        {
            if(e.Type == Models.Enums.NavigationType.Player)
            {
                RootFrame.Navigate(typeof(PlayerPage), e.Parameter);
            }
            else if(RootFrame.SourcePageType == typeof(PlayerPage))
            {
                RootFrame.Navigate(typeof(MainPage), e.Parameter);
            }
        }
    }
}
