using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.QingTing.Network;
using PureRadio.Uwp.Models.QingTing.User;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
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
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private SplashScreen splash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.

        public RootPage(SplashScreen splashscreen, bool loadState)
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

            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This ensures that the extended splash screen formats properly in response to window resizing.
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            splash = splashscreen;
            if (splash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed.
                splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;

                extendedSplash.Visibility = Visibility.Visible;

                PositionImage();

                PositionRing();
            }

            // Restore the saved session state if necessary
            RestoreState(loadState);
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
            if(e.Type == NavigationType.Player)
            {
                if ((bool?)e.Parameter ?? true)
                {
                    if (RootFrame.CanGoBack)
                        RootFrame.GoBack();
                    else
                        RootFrame.Navigate(typeof(MainPage), e.TransitionInfo);
                }                    
                else
                    RootFrame.Navigate(typeof(PlayerPage), e.TransitionInfo) ;
            }
            else if(RootFrame.SourcePageType != typeof(MainPage))
            {
                if (RootFrame.CanGoBack)
                    RootFrame.GoBack();
                else
                    RootFrame.Navigate(typeof(MainPage), e.TransitionInfo);
                var navigate = Ioc.Default.GetRequiredService<INavigateService>();
                if (e.PageId == PageIds.RadioDetail || e.PageId == PageIds.ContentDetail)
                    navigate.NavigateToSecondaryView(e.PageId, e.TransitionInfo, e.Parameter);
            }
        }

        private void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            extendedSplashImage.Height = splashImageRect.Height;
            extendedSplashImage.Width = splashImageRect.Width;
        }

        private void PositionRing()
        {
            splashProgressRing.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.5) - (splashProgressRing.Width * 0.5));
            splashProgressRing.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height + splashImageRect.Height * 0.1));
        }

        // Include code to be executed when the system has transitioned from the splash screen to the extended splash screen (application's first view).
        private async void DismissedEventHandler(SplashScreen sender, object e)
        {
            dismissed = true;

            // Complete app setup operations here...

            await Ioc.Default.GetRequiredService<ISqliteService>().InitializeDatabaseAsync();
            
            try
            {
                var httpProvider = Ioc.Default.GetRequiredService<IHttpProvider>();
                var request = await httpProvider.GetRequestMessageAsync(ApiConstants.Network.IpAddr, HttpMethod.Get, null, RequestType.Default, false, false);
                var response = await httpProvider.SendAsync(request);
                var result = await httpProvider.ParseAsync<IPAddrResponse>(response);
                if (result?.Code == 0)
                {

                }
            }
            catch(Exception)
            {
                
            }

            ImageCache.Instance.CacheDuration = TimeSpan.FromHours(24);
            ImageCache.Instance.MaxMemoryCacheCount = 100;


            await Ioc.Default.GetRequiredService<IAccountProvider>().TrySignInAsync();
            DismissExtendedSplash();
        }

        private async void DismissExtendedSplash()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                extendedSplash.Visibility = Visibility.Collapsed;

                RootFrame.Navigate(typeof(MainPage));
            });
        }

        private void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be executed when a user resizes the window.
            if (splash != null)
            {
                // Update the coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;
                PositionImage();

                PositionRing();
            }
        }

        private void RestoreState(bool loadState)
        {
            if (loadState)
            {
                // code to load your app's state here
            }
        }
    }
}
