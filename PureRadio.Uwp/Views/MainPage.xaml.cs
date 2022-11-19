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
using PureRadio.Uwp.Views.Secondary;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Uwp.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly List<(string Tag, PageIds pageIds, Type Page)> _mainPages = new List<(string Tag, PageIds pageIds, Type Page)>
        {
            (((int)PageIds.Home).ToString(), PageIds.Home, typeof(HomePage)),
            //(((int)PageIds.Radio).ToString(), typeof(CategoriesPage)),
            //(((int)PageIds.Content).ToString(), typeof(RankPage)),
            (((int)PageIds.Library).ToString(),PageIds.Library, typeof(LibraryPage)),
            (((int)PageIds.Settings).ToString(), PageIds.Settings, typeof(SettingsPage))
        };

        private readonly List<(string Tag, PageIds pageIds, Type Page)> _secondaryPages = new List<(string Tag, PageIds pageIds, Type Page)>
        {
            (((int)PageIds.Search).ToString(), PageIds.Search, typeof(SearchPage)),
            //(((int)PageIds.Radio).ToString(), typeof(CategoriesPage)),
            //(((int)PageIds.Content).ToString(), typeof(RankPage)),
            //(((int)PageIds.Library).ToString(), typeof(ContentPage)),
            //(((int)PageIds.Settings).ToString(),typeof(SettingsPage))
        };

        private PageIds _currentPageId;

        public MainViewModel ViewModel => (MainViewModel)DataContext;
        public readonly NativePlayerViewModel PlayerViewModel;

        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;

            DataContext = Ioc.Default.GetRequiredService<MainViewModel>();
            PlayerViewModel = Ioc.Default.GetRequiredService<NativePlayerViewModel>();

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(TitleBarHost);

            // Register a handler for when the title bar visibility changes.
            // For example, when the title bar is invoked in full screen mode.
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;

            // Register a handler for when the window activation changes.
            Window.Current.CoreWindow.Activated += CoreWindow_Activated;

            MediaPosition.AddHandler(PointerReleasedEvent, new PointerEventHandler(MediaPositionLive_PointerReleased), true);
            VolumeControl.AddHandler(PointerReleasedEvent, new PointerEventHandler(VolumeControl_PointerReleased), true);

            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            // Ioc.Default.GetRequiredService<INavigateService>().Navigating -= MainPage_Navigating;
            Ioc.Default.GetRequiredService<ISettingsService>().SettingTheme -= MainPage_SettingTheme;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Ioc.Default.GetRequiredService<INavigateService>().Navigating += MainPage_Navigating;
            Ioc.Default.GetRequiredService<ISettingsService>().SettingTheme += MainPage_SettingTheme;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimation animation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("PlayerToMainAni");
                if (animation != null)
                {
                    animation.TryStart(Cover);
                }

                ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
                Color foreground = App.RootTheme switch
                {
                    ElementTheme.Dark => Colors.White,
                    ElementTheme.Light => Colors.Black,
                    _ => Windows.UI.Xaml.Application.Current.RequestedTheme == ApplicationTheme.Dark ? Colors.White : Colors.Black,
                };
                titleBar.ButtonForegroundColor = foreground;
                AppTitleTextBlock.Foreground = new SolidColorBrush(foreground);
            }
            Ioc.Default.GetRequiredService<INavigateService>().Navigating += MainPage_Navigating;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Ioc.Default.GetRequiredService<INavigateService>().Navigating -= MainPage_Navigating;

            base.OnNavigatedFrom(e);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TryGoBack();
        }

        private void MainPage_Navigating(object sender, Models.Args.AppNavigationEventArgs e)
        {
            if(e.Type == NavigationType.Main)
            {
                NavigateToMainView(e.PageId, e.TransitionInfo, e.Parameter);
            }
            else if(e.Type == NavigationType.Secondary)
            {
                NavigateToSecondaryView(e.PageId, e.TransitionInfo, e.Parameter);
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
            if(NavView.SelectedItem == null)
                NavigateToMainView(PageIds.Home, new EntranceNavigationTransitionInfo());
            // Listen to the window directly so the app responds
            // to accelerator keys regardless of which element has focus.
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated +=
                CoreDispatcher_AcceleratorKeyActivated;

            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;

            SystemNavigationManager.GetForCurrentView().BackRequested += System_BackRequested;
        }


        private void NavigateToMainView(PageIds pageId, NavigationTransitionInfo transitionInfo, object parameter = null)
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
                    pageType = typeof(LibraryPage);
                    break;
                case PageIds.Settings:
                    pageType = typeof(SettingsPage);
                    break;
            }
            if (pageType != null)
            {
                if(_currentPageId != pageId)
                {
                    ContentFrame.Navigate(pageType, parameter, transitionInfo);
                    _currentPageId = pageId;
                }
            }
        }

        private void NavigateToSecondaryView(PageIds pageId, NavigationTransitionInfo transitionInfo, object parameter = null)
        {
            Type pageType = null;
            switch (pageId)
            {
                case PageIds.None:
                default:
                    break;
                case PageIds.User:
                    
                    break;
                case PageIds.Search:
                    pageType = typeof(SearchPage);
                    break;
                case PageIds.RadioDetail:
                    pageType = typeof(RadioDetailPage);
                    break;
                case PageIds.ContentDetail:
                    pageType = typeof(ContentDetailPage);
                    break;
                case PageIds.RadioCategory:
                    pageType = typeof(RadioCategoryPage);
                    break;
                case PageIds.ContentCategory:
                    pageType = typeof(ContentCategoryPage);
                    break;
            }
            if (pageType != null)
            {
                ContentFrame.Navigate(pageType, parameter, transitionInfo);
                _currentPageId = pageId;
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
            var item = _mainPages.FirstOrDefault(p => p.Page == e.SourcePageType);
            
            if (NavView.SelectedItem == null ||
                (NavView.SelectedItem is muxc.NavigationViewItem navItem && item.Tag != null &&
                navItem.Tag.ToString() != item.Tag))
            {
                var shouldSelectedItem = NavView.MenuItems.Concat(NavView.FooterMenuItems).OfType<muxc.NavigationViewItem>().Where(
                    n => n.Tag.Equals(item.Tag)
                    ).FirstOrDefault();
                _currentPageId = item.pageIds;
                NavView.SelectedItem = shouldSelectedItem;
            }
            else
            {
                item = _secondaryPages.FirstOrDefault(p => p.Page == e.SourcePageType);
                if (item.Tag != null) _currentPageId = item.pageIds;
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
            DynamicBackground.Visibility = Visibility.Visible;
        }

        private void PlayerContainer_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            DynamicBackground.Visibility = Visibility.Collapsed;
        }

        private void PlayerContainer_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (PlayerViewModel.ShowElement)
            {
                ConnectedAnimationService.GetForCurrentView()
                    .PrepareToAnimate("MainToPlayerAni", Cover);
                ViewModel.Navigate(PageIds.Player);
            }                
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
            string keyword = string.Empty;
            if (args.ChosenSuggestion != null)
            {
                keyword = (string)args.ChosenSuggestion;
                // User selected an item from the suggestion list, take an action on it here.                
                // ContentFrame.Navigate(typeof(SearchResultPage), args.ChosenSuggestion.ToString());
            }
            else if (!string.IsNullOrEmpty(args.QueryText))
            {
                keyword = args.QueryText;
                // ContentFrame.Navigate(typeof(SearchResultPage), args.QueryText);
            }
            if (!string.IsNullOrEmpty(keyword) && keyword != ViewModel._noResultTip) 
            {
                ViewModel.Navigate(PageIds.Search, keyword);
                ViewModel.Keyword = string.Empty;
            }
        }

        private void LoginDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            revealModeCheckBox.IsChecked = false;
            usernameBox.Text = string.Empty;
            passworBox.Password = string.Empty;
        }

        private void revealModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            passworBox.PasswordRevealMode = PasswordRevealMode.Visible;
        }

        private void revealModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            passworBox.PasswordRevealMode = PasswordRevealMode.Hidden;
        }

        private async void AccountStateButoon_Click(object sender, RoutedEventArgs e)
        {
            if(ViewModel.AccountState == AuthorizeState.SignedOut)
            {
                LoginDialog.RequestedTheme = App.RootTheme;
                ContentDialogResult result = await LoginDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    string username = usernameBox.Text;
                    string password = passworBox.Password;
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        LoginDialogTeachingTip.IsOpen = true;
                    }
                    else
                    {
                        if (!await ViewModel.TrySignIn(username, password)) 
                            LoginDialogFailureTip.IsOpen = true;
                        else 
                            LoginDialogSuccessTip.IsOpen = true;
                    }
                }
            }
            else
            {
                if (await ViewModel.TrySignOut()) LogoutDialogSuccessTip.IsOpen = true;
            }
        }

        private void MediaPositionLive_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PlayerViewModel.SetPosition((int)(sender as Slider).Value);
        }

        private void MediaPositionLive_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            PlayerViewModel.IsMoveMediaPosition = true;
        }

        private void MediaPositionLive_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PlayerViewModel.IsMoveMediaPosition = false;
        }

        private void MediaPositionLive_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            PlayerViewModel.SetPosition((int)(sender as Slider).Value);
        }

        private void VolumeControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PlayerViewModel.SetVolume((sender as Slider).Value);
        }

        private void VolumeControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            PlayerViewModel.SetVolume((sender as Slider).Value);
        }

        private void VolumeControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            PlayerViewModel.IsMoveVolume = true;
        }

        private void VolumeControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PlayerViewModel.IsMoveVolume = false;
        }
    }
}
