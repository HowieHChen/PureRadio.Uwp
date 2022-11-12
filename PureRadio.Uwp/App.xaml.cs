using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;
using PureRadio.Uwp.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using System.ComponentModel.Design;
using Microsoft.Extensions.DependencyInjection;
using PureRadio.Uwp.Services.Interfaces;
using PureRadio.Uwp.Services;
using Windows.Globalization;
using PureRadio.Uwp.ViewModels;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Providers;
using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Adapters;

namespace PureRadio.Uwp
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// 根主题
        /// </summary>
        public static ElementTheme RootTheme
        {
            get
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    return rootElement.RequestedTheme;
                }

                return ElementTheme.Default;
            }
            set
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = value;
                }
            }
        }
        /// <summary>
        /// 主题切换支持
        /// </summary>

        public void CheckTheme(ISettingsService settings)
        {
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            string strCurrentTheme = (settings.GetValue<string>(AppConstants.SettingsKey.ConfigTheme) ?? AppConstants.SettingsValue.Default);
            switch (strCurrentTheme)
            {
                case AppConstants.SettingsValue.Light:
                    RootTheme = ElementTheme.Light;
                    titleBar.ButtonForegroundColor = Colors.Black;
                    break;
                case AppConstants.SettingsValue.Dark:
                    RootTheme = ElementTheme.Dark;
                    titleBar.ButtonForegroundColor = Colors.White;
                    break;
                default:
                    if (Current.RequestedTheme == ApplicationTheme.Dark)
                    {
                        titleBar.ButtonForegroundColor = Colors.White;
                    }
                    else
                    {
                        titleBar.ButtonForegroundColor = Colors.Black;
                    }
                    break;
            }
        }

        /// <summary>
        /// 多语言支持
        /// </summary>
        void CheckLanguage(ISettingsService settings)
        {
            string strCurrentLanguage = (settings.GetValue<string>(AppConstants.SettingsKey.ConfigLanguage) ?? AppConstants.SettingsValue.Auto);
            if (strCurrentLanguage == AppConstants.SettingsValue.Auto)
                ApplicationLanguages.PrimaryLanguageOverride = string.Empty;
            else
                ApplicationLanguages.PrimaryLanguageOverride = strCurrentLanguage;
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
                
                Ioc.Default.ConfigureServices(
                        new ServiceCollection()
                        // Services
                        .AddSingleton<ISettingsService, SettingsService>()
                        .AddSingleton<INavigateService, NavigateService>()
                        // Adapters
                        .AddSingleton<IAccountAdapter, AccountAdapter>()
                        .AddSingleton<IContentAdapter, ContentAdapter>()
                        .AddSingleton<IRadioAdapter, RadioAdapter>()
                        .AddSingleton<ISearchAdapter, SearchAdapter>()
                        // Providers
                        .AddSingleton<IAccountProvider, AccountProvider>()
                        .AddSingleton<IHttpProvider, HttpProvider>()
                        .AddSingleton<ISearchProvider, SearchProvider>()
                        .AddSingleton<IRadioProvider, RadioProvider>()
                        .AddSingleton<IContentProvider, ContentProvider>()
                        // Viewmodels
                        .AddTransient<MainViewModel>()
                        .AddTransient<SettingsViewModel>()
                        .AddTransient<SearchViewModel>()
                        .AddTransient<RadioDetailViewModel>()
                        .AddTransient<ContentDetailViewModel>()
                        // Build
                        .BuildServiceProvider());
            }

            if (e.PrelaunchActivated == false)
            {
                ISettingsService settings = Ioc.Default.GetRequiredService<ISettingsService>();
                CheckLanguage(settings);
                if (rootFrame.Content == null)
                {
                    bool loadState = (e.PreviousExecutionState == ApplicationExecutionState.Terminated);
                    RootPage extendedSplash = new RootPage(e.SplashScreen, loadState);
                    Window.Current.Content = extendedSplash;
                    // 当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    // rootFrame.Navigate(typeof(RootPage), e.Arguments);
                }
                
                // 确保当前窗口处于活动状态
                Window.Current.Activate();
                // Switch theme
                CheckTheme(settings);
            }
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }
    }
}
