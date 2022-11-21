using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Controls;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Uwp.Views.Main
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel => (HomeViewModel)DataContext;

        public HomePage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<HomeViewModel>();

        }

        private void RecRadioLiveResult_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is RadioInfoDetail radioInfo)
            {
                (sender as AdaptiveGridView).PrepareConnectedAnimation("RadioToDetailAni", radioInfo, "RecRadioLiveCover");
                ViewModel.Navigate(PageIds.RadioDetail, radioInfo.RadioId);
            }
        }

        private void RecRadioReplayResult_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is RadioReplayInfo contentInfo)
            {
                (sender as AdaptiveGridView).PrepareConnectedAnimation("ContentToDetailAni", contentInfo, "RecRadioReplayCover");
                ViewModel.Navigate(PageIds.ContentDetail, contentInfo.ContentId);
            }
        }

        private void RecContentResult_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is ContentInfoCategory contentInfo)
            {
                (sender as AdaptiveGridView).PrepareConnectedAnimation("ContentToDetailAni", contentInfo, "RecContentCover");
                ViewModel.Navigate(PageIds.ContentDetail, contentInfo.ContentId);
            }
        }
    }
}
