using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Controls;
using PureRadio.Uwp.Models.Args;
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

namespace PureRadio.Uwp.Views.Secondary
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ContentCategoryPage : Page
    {
        public ContentCategoryViewModel ViewModel => (ContentCategoryViewModel)DataContext;
        public ContentCategoryPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<ContentCategoryViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var Parameters = (ContentCategoryEventArgs)e.Parameter;
            ViewModel.AttrId = Parameters?.AttributeId ?? 0;
            ViewModel.CategoryTitle = Parameters?.CategoryTitle ?? string.Empty;
            ViewModel.CategoryId = Parameters?.CategoryId ?? 0;
        }

        private void AdaptiveGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is ContentInfoCategory contentInfo)
            {
                (sender as AdaptiveGridView).PrepareConnectedAnimation("ContentToDetailAni", contentInfo, "ContentCover");
                ViewModel.Navigate(PageIds.ContentDetail, contentInfo.ContentId);
            }
        }
    }
}
