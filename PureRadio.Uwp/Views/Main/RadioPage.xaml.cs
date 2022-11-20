using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Controls;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
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
    public sealed partial class RadioPage : Page
    {
        public RadioViewModel ViewModel => (RadioViewModel)DataContext;
        public RadioPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<RadioViewModel>();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }

        private void RecommendResult_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is RadioInfoRecommend radioInfo)
            {
                (sender as AdaptiveGridView).PrepareConnectedAnimation("RadioToDetailAni", radioInfo, "RecCover");
                ViewModel.Navigate(PageIds.RadioDetail, radioInfo.RadioId);
            }
        }

        private void NetTrendResult_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is RadioInfoSummary radioInfo)
            {
                (sender as AdaptiveGridView).PrepareConnectedAnimation("RadioToDetailAni", radioInfo, "TrendCover");
                ViewModel.Navigate(PageIds.RadioDetail, radioInfo.RadioId);
            }
        }

        private void LocalTrendResult_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is RadioInfoSummary radioInfo)
            {
                (sender as AdaptiveGridView).PrepareConnectedAnimation("RadioToDetailAni", radioInfo, "TrendCover");
                ViewModel.Navigate(PageIds.RadioDetail, radioInfo.RadioId);
            }
        }

        private void RadioCategory_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is RadioCategoryItem category)
            {
                ViewModel.Navigate(PageIds.RadioCategory, category.CategoryId);
            }
        }
    }
}
