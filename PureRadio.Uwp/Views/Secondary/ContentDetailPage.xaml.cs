﻿using CommunityToolkit.Mvvm.DependencyInjection;
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
    public sealed partial class ContentDetailPage : Page
    {
        public ContentDetailViewModel ViewModel => (ContentDetailViewModel)DataContext;

        public ContentDetailPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<ContentDetailViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.ContentId = (int)e.Parameter;
        }

        private void PlayListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
