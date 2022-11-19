using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Animations;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Uwp.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PlayerPage : Page
    {
        public FullScreenPlayerViewModel ViewModel => (FullScreenPlayerViewModel)DataContext;
        public PlayerPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<FullScreenPlayerViewModel>();

            // CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            // Set XAML element as a drag region.
            Window.Current.SetTitleBar(TitleBarHost);

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            Color foreground = Colors.White;
            titleBar.ButtonForegroundColor = foreground;

            MediaPosition.AddHandler(PointerReleasedEvent, new PointerEventHandler(MediaPositionLive_PointerReleased), true);
            VolumeControl.AddHandler(PointerReleasedEvent, new PointerEventHandler(VolumeControl_PointerReleased), true);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.PlayerStateChanged -= PlayerStateChanged;
            
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation animation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("MainToPlayerAni");
            if (animation != null)
            {
                animation.TryStart(Cover);
                animation.Completed += Animation_Completed;
            }
        }

        private void Animation_Completed(ConnectedAnimation sender, object args)
        {
            ViewModel.PlayerStateChanged += PlayerStateChanged;
            PlayerStateChanged(this, ViewModel.PlayerState);
            sender.Completed -= Animation_Completed;
        }

        private void PlayerStateChanged(object sender, MediaPlaybackState e)
        {
            switch (e)
            {
                default:
                case MediaPlaybackState.None:
                case MediaPlaybackState.Opening:
                    break;
                case MediaPlaybackState.Buffering:
                    ScaleOutTrans.Begin();
                    break;
                case MediaPlaybackState.Playing:
                    ScaleOutTrans.Begin();
                    break;
                case MediaPlaybackState.Paused:
                    ScaleInTrans.Begin();
                    break;
            }
        }

        private void MediaPosition_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.SetPosition((int)(sender as Slider).Value);
        }

        private void MediaPosition_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ViewModel.IsMoveMediaPosition = true;
        }

        private void MediaPosition_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ViewModel.IsMoveMediaPosition = false;
        }

        private void MediaPositionLive_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ViewModel.SetPosition((int)(sender as Slider).Value);
        }

        private void VolumeControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.SetVolume((sender as Slider).Value);
        }

        private void VolumeControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ViewModel.IsMoveVolume = false;
        }

        private void VolumeControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ViewModel.IsMoveVolume = true;
        }

        private void VolumeControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ViewModel.SetVolume((sender as Slider).Value);
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.PlayerState == MediaPlaybackState.Paused)
            {
                CoverImage.Width = CoverImage.Height = Cover.Height = Cover.Width = 160;
                Cover.Margin = new Thickness(20, 36, 20, 36);
                ScaleTrans.ScaleX = ScaleTrans.ScaleY = 1;
            }
                
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("PlayerToMainAni", Cover);
            ViewModel.NavigateBack();
        }

        private void PlayListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as PlayItemSnapshot;
            var list = sender as ListView;
            int index = list.Items.IndexOf(item);
            ViewModel.PlayItem(item, index);
        }

        private void NavDetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.PlayerState == MediaPlaybackState.Paused)
            {
                CoverImage.Width = CoverImage.Height = Cover.Height = Cover.Width = 160;
                Cover.Margin = new Thickness(20, 36, 20, 36);
                ScaleTrans.ScaleX = ScaleTrans.ScaleY = 1;
            }

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("PlayerToMainAni", Cover);
            ViewModel.NavigateDetail();
        }
    }
}
