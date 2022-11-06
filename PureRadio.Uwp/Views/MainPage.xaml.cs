using muxc = Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CommunityToolkit.Mvvm.DependencyInjection;
using PureRadio.Uwp.ViewModels;
using System.ComponentModel.Design.Serialization;
using PureRadio.Uwp.Services.Interfaces;
using PureRadio.Uwp.Models.Enums;
using static System.Net.Mime.MediaTypeNames;
using Windows.UI.Xaml.Media.Animation;
using PureRadio.Uwp.Views.Main;
using Windows.UI;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Uwp.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            (((int)PageIds.Home).ToString(), typeof(HomePage)),
            //(((int)PageIds.Radio).ToString(), typeof(CategoriesPage)),
            //(((int)PageIds.Content).ToString(), typeof(RankPage)),
            //(((int)PageIds.Library).ToString(), typeof(ContentPage)),
            (((int)PageIds.Settings).ToString(),typeof(SettingsPage))
        };

        private PageIds _currentPageId;

        public MainViewModel ViewModel => (MainViewModel)DataContext;

        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;

            DataContext = Ioc.Default.GetRequiredService<MainViewModel>();

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(TitleBarHost);

            // Register a handler for when the title bar visibility changes.
            // For example, when the title bar is invoked in full screen mode.
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;

            // Register a handler for when the window activation changes.
            Window.Current.CoreWindow.Activated += CoreWindow_Activated;

        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Ioc.Default.GetRequiredService<INavigateService>().Navigating -= MainPage_Navigating;
            Ioc.Default.GetRequiredService<ISettingsService>().SettingTheme -= MainPage_SettingTheme;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Ioc.Default.GetRequiredService<INavigateService>().Navigating += MainPage_Navigating;
            Ioc.Default.GetRequiredService<ISettingsService>().SettingTheme += MainPage_SettingTheme;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TryGoBack();
        }

        private void MainPage_Navigating(object sender, Models.Args.AppNavigationEventArgs e)
        {
            if(e.Type == NavigationType.Main)
            {
                NavigateToMainView(e.PageId, e.Parameter);
            }
        }


        private void MainPage_SettingTheme(object sender, ElementTheme e)
        {
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            App.RootTheme = e;
            Color foreground = e switch
            {
                ElementTheme.Dark => Colors.White,
                ElementTheme.Light => Colors.Black,
                _ => Windows.UI.Xaml.Application.Current.RequestedTheme == ApplicationTheme.Dark ? Colors.White : Colors.Black,
            };
            titleBar.ButtonForegroundColor = foreground;
            AppTitleTextBlock.Foreground = new SolidColorBrush(foreground);
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (sender.IsVisible)
            {
                TitleBarHost.Visibility = ContentGrid.Visibility = Visibility.Visible;
            }
            else
            {
                TitleBarHost.Visibility = ContentGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void CoreWindow_Activated(CoreWindow sender, WindowActivatedEventArgs args)
        {
            UISettings settings = new UISettings();
            if (args.WindowActivationState == CoreWindowActivationState.Deactivated)
            {
                AppTitleTextBlock.Foreground =
                   new SolidColorBrush(settings.UIElementColor(UIElementType.GrayText));
            }
            else
            {
                AppTitleTextBlock.Foreground =
                    new SolidColorBrush(App.RootTheme == ElementTheme.Dark? Color.FromArgb(255,255,255,255) : Color.FromArgb(255,0,0,0));
            }
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            // Add handler for ContentFrame navigation.
            ContentFrame.Navigated += On_Navigated;
            // NavView doesn't load any page by default, so load home page.
            NavigateToMainView(PageIds.Home);
            // Listen to the window directly so the app responds
            // to accelerator keys regardless of which element has focus.
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated +=
                CoreDispatcher_AcceleratorKeyActivated;

            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;

            SystemNavigationManager.GetForCurrentView().BackRequested += System_BackRequested;
        }


        private void NavigateToMainView(PageIds pageId, object parameter = null)
        {
            Type pageType = null;
            switch (pageId)
            {
                case PageIds.None:
                default:
                    break;
                case PageIds.Home: 
                    pageType = typeof(HomePage);
                    break;
                case PageIds.Radio:

                    break;
                case PageIds.Content:

                    break;
                case PageIds.Library:

                    break;
                case PageIds.Settings:
                    pageType = typeof(SettingsPage);
                    break;
                case PageIds.User:

                    break;
                case PageIds.Search:

                    break;
            }
            if (pageType != null)
            {
                if(_currentPageId != pageId)
                {
                    ContentFrame.Navigate(pageType, parameter, new EntranceNavigationTransitionInfo());
                    _currentPageId = pageId;
                }
            }
        }


        private void NavigationView_ItemInvoked(
            muxc.NavigationView sender,
            muxc.NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var pageId = (PageIds)Enum.Parse(typeof(PageIds), args.InvokedItemContainer.Tag.ToString());
                ViewModel.Navigate(pageId);
            }
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);
            if (NavView.SelectedItem == null ||
                (NavView.SelectedItem is muxc.NavigationViewItem navItem && item.Tag != null &&
                navItem.Tag.ToString() != item.Tag))
            {
                var shouldSelectedItem = NavView.MenuItems.Concat(NavView.FooterMenuItems).OfType<muxc.NavigationViewItem>().Where(
                    n => n.Tag.Equals(item.Tag)
                    ).FirstOrDefault();
                _currentPageId = (PageIds)Enum.Parse(typeof(PageIds), item.Tag.ToString());
                NavView.SelectedItem = shouldSelectedItem;
            }
        }

        private bool TryGoBack()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == muxc.NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == muxc.NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

        private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
        {
            // When Alt+Left are pressed navigate back
            if (e.EventType == CoreAcceleratorKeyEventType.SystemKeyDown
                && e.VirtualKey == VirtualKey.Left
                && e.KeyStatus.IsMenuKeyDown == true
                && !e.Handled)
            {
                e.Handled = TryGoBack();
            }
        }

        private void System_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = TryGoBack();
            }
        }

        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
        {
            // Handle mouse back button.
            if (e.CurrentPoint.Properties.IsXButton1Pressed)
            {
                e.Handled = TryGoBack();
            }
        }

        private void PlayerContainer_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        private void PlayerContainer_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void PlayerContainer_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Navigate(PageIds.Player);
        }

        private void BackButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                AppTitleContainer.Margin = new Thickness(8, 0, 4, 0);
            }
            else
            {
                AppTitleContainer.Margin = new Thickness(18, 0, 22, 0);
            }
                
        }

        private void AppTitleSearchBar_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.                
                // ContentFrame.Navigate(typeof(SearchResultPage), args.ChosenSuggestion.ToString());
            }
            else if (!string.IsNullOrEmpty(args.QueryText))
            {
                // ContentFrame.Navigate(typeof(SearchResultPage), args.QueryText);
            }
        }

        private void AppTitleSearchBar_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

        }
    }
}
