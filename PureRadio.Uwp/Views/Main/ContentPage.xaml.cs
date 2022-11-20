using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Controls;
using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Database;
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
    public sealed partial class ContentPage : Page
    {
        public ContentViewModel ViewModel => (ContentViewModel)DataContext;

        ScrollViewer listScrollViewer;
        public ContentPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<ContentViewModel>();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }

        private void CategoryItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is ContentInfoRecommend content)
            {
                (sender as AdaptiveGridView).PrepareConnectedAnimation("ContentToDetailAni", content, "ContentCover");
                ViewModel.Navigate(PageIds.ContentDetail, content.ContentId);
            }
        }

        private void SeeAllkButton_Click(object sender, RoutedEventArgs e)
        {
            ContentRecommendSet recSet = ((HyperlinkButton)sender).DataContext as ContentRecommendSet;
            var param = new ContentCategoryEventArgs(recSet.CategoryId, 0, recSet.CategoryTittle);
            ViewModel.Navigate(PageIds.ContentCategory, param);
        }

        private void CategoryItemsList_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            var gridScrollViewer = (VisualTreeHelper.GetChild((AdaptiveGridView)sender, 0) as Border).Child as ScrollViewer;
            int delta = e.GetCurrentPoint(sender as UIElement).Properties.MouseWheelDelta;
            if (e.KeyModifiers == Windows.System.VirtualKeyModifiers.Control || e.KeyModifiers == Windows.System.VirtualKeyModifiers.Shift)
                gridScrollViewer.ChangeView(gridScrollViewer.HorizontalOffset - delta, gridScrollViewer.VerticalOffset, 1);
            else listScrollViewer.ChangeView(listScrollViewer.HorizontalOffset, listScrollViewer.VerticalOffset - delta, 1);
            e.Handled = true;
        }

        private void CategoriesListView_Loaded(object sender, RoutedEventArgs e)
        {
            listScrollViewer = (VisualTreeHelper.GetChild(CategoriesListView, 0) as Border).Child as ScrollViewer;
        }
    }
}
