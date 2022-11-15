using CommunityToolkit.Mvvm.DependencyInjection;
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
    public sealed partial class RadioDetailPage : Page
    {
        public RadioDetailViewModel ViewModel => (RadioDetailViewModel)DataContext;

        public RadioDetailPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<RadioDetailViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.RadioId = (int)e.Parameter;
        }

        private void PlayListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as RadioPlaylistDetail;
            var list = sender as ListView;
            int index = list.Items.IndexOf(item);
            ViewModel.PlayRadioDemand(index);
        }

        private void NavList_Loaded(object sender, RoutedEventArgs e)
        {
            // NavView doesn't load any page by default, so load home page.
            NavList.SelectedItem = NavList.MenuItems[2];
            // If navigation occurs on SelectionChanged, this isn't needed.
            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the home page.
            NavView_Navigate("TODAY");
        }

        private void NavView_Navigate(string navItemTag)
        {
            PlaylistDay target = navItemTag switch
            {
                "BYDAY" => PlaylistDay.BeforeYesterday,
                "YDAY" => PlaylistDay.Yesterday,
                "TMR" => PlaylistDay.Tomorrow,
                _ => PlaylistDay.Today,
            };
            ViewModel.SwitchPlaylistSource(target);
        }

        private void NavList_ItemInvoked(
            Microsoft.UI.Xaml.Controls.NavigationView sender,
            Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag);
            }
        }
    }
}
