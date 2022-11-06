using CommunityToolkit.Mvvm.DependencyInjection;
using PureRadio.Uwp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
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
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel ViewModel => (SettingsViewModel)DataContext;

        public SettingsPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<SettingsViewModel>();

            Loaded += SettingsPage_Loaded;
            Unloaded += SettingsPage_Unloaded;
        }

        private void SettingsPage_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowRestartDialog -= ShowRestartDialogAsync;
        }

        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowRestartDialog += ShowRestartDialogAsync;
        }

        private async void ShowRestartDialogAsync(object sender, EventArgs e)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            ContentDialog restartDialog = new ContentDialog
            {
                Title = resourceLoader.GetString("PageSettingsRestartDialog/Title"),
                Content = resourceLoader.GetString("PageSettingsRestartDialog/Content"),
                PrimaryButtonText = resourceLoader.GetString("PageSettingsRestartDialog/PrimaryButtonText"),
                CloseButtonText = resourceLoader.GetString("PageSettingsRestartDialog/CloseButtonText"),
                DefaultButton = ContentDialogButton.Primary,
                RequestedTheme = ViewModel.Theme
            };
            ContentDialogResult result = await restartDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                await ViewModel.RestartAsync();
            }
            else
            {
                ViewModel.ResetLanguageSelected();
            }
        }
    }
}
