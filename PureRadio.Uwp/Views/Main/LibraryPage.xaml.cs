using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Controls;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Database;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Radios;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static PureRadio.Uwp.Models.Data.Constants.ApiConstants;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Uwp.Views.Main
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LibraryPage : Page
    {
        public LibraryViewModel ViewModel => (LibraryViewModel)DataContext;
        public LibraryPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<LibraryViewModel>();

        }

        private void RadioListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is FavRadio radio)
            {
                (sender as ListView).PrepareConnectedAnimation("RadioToDetailAni", radio, "FavRadioCover");
                ViewModel.Navigate(PageIds.RadioDetail, radio.MainId);
            }
        }

        private void ContentListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is FavContent content)
            {
                (sender as ListView).PrepareConnectedAnimation("ContentToDetailAni", content, "FavContentCover");
                ViewModel.Navigate(PageIds.ContentDetail, content.MainId);
            }
        }

        private void HistoryListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is History history)
            {
                if(history.Type == MediaPlayType.ContentDemand)
                {
                    (sender as ListView).PrepareConnectedAnimation("ContentToDetailAni", history, "HistoryCover");
                    ViewModel.Navigate(PageIds.ContentDetail, history.MainId);
                }
                else
                {
                    (sender as ListView).PrepareConnectedAnimation("RadioToDetailAni", history, "HistoryCover");
                    ViewModel.Navigate(PageIds.RadioDetail, history.MainId);
                }
            }
        }

        private void RadioItemButton_Click(object sender, RoutedEventArgs e)
        {
            FavRadio radio = ((Button)sender).DataContext as FavRadio;
            ViewModel.RemoveFavRadio(radio);
        }

        private void ContentItemButton_Click(object sender, RoutedEventArgs e)
        {
            FavContent content = ((Button)sender).DataContext as FavContent;
            ViewModel.RemoveFavContent(content);
        }
    }
}
