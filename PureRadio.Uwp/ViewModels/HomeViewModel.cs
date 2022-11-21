using CommunityToolkit.Mvvm.ComponentModel;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class HomeViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IRadioProvider radioProvider;
        private readonly IContentProvider contentProvider;
        private readonly DispatcherTimer _refreshTimer;

        [ObservableProperty]
        private bool _isRecRadioLiveLoading;
        [ObservableProperty]
        private bool _isRecRadioLiveEmpty;
        [ObservableProperty]
        private List<RadioInfoDetail> _listRecRadioLive;

        [ObservableProperty]
        private bool _isRecRadioReplayLoading;
        [ObservableProperty]
        private bool _isRecRadioReplayEmpty;
        [ObservableProperty]
        private List<RadioReplayInfo> _listRecRadioReplay;

        [ObservableProperty]
        private bool _isRecContentLoading;
        [ObservableProperty]
        private bool _isRecContentEmpty;
        [ObservableProperty]
        private List<ContentInfoCategory> _listRecContent;

        public HomeViewModel(
            INavigateService navigate, 
            IRadioProvider radioProvider, 
            IContentProvider contentProvider)
        {
            this.navigate = navigate;
            this.radioProvider = radioProvider;
            this.contentProvider = contentProvider;
            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(3),
            };
            LoadData();
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            _refreshTimer.Tick += OnRefreshTimerTick;
            if (!_refreshTimer.IsEnabled)
                _refreshTimer.Start();
        }

        protected override void OnDeactivated()
        {
            if (_refreshTimer.IsEnabled)
                _refreshTimer.Stop();
            _refreshTimer.Tick -= OnRefreshTimerTick;
            base.OnDeactivated();
        }

        private void OnRefreshTimerTick(object sender, object e)
        {
            int minute = DateTime.Now.Minute;
            if ((0 < minute && minute <= 3) || (30 < minute && minute <= 33))
            {
                LoadData(false);
            }            
        }

        public async void LoadData(bool loadContent = true)
        {
            IsRecRadioLiveLoading = IsRecRadioReplayLoading = true;
            var result = await radioProvider.GetRadioHomeRecResult(CancellationToken.None);
            ListRecRadioLive = result.Item1;
            IsRecRadioLiveEmpty = ListRecRadioLive.Count == 0;
            IsRecRadioLiveLoading = false;

            ListRecRadioReplay = result.Item2;
            IsRecRadioReplayEmpty = ListRecRadioReplay.Count == 0;
            IsRecRadioReplayLoading = false;

            if (loadContent) LoadContent();
        }

        private async void LoadContent()
        {
            IsRecContentLoading = true;
            var contents = await contentProvider.GetContentCategoryResult(545, CancellationToken.None);
            ListRecContent = contents.Items.ToList();
            IsRecContentEmpty = ListRecContent.Count == 0;
            IsRecContentLoading = false;
        }

        public void Navigate(PageIds pageId, object parameter = null)
        {
            if (pageId == PageIds.RadioDetail || pageId == PageIds.ContentDetail)
            {
                navigate.NavigateToSecondaryView(pageId, new EntranceNavigationTransitionInfo(), parameter);
            }
        }

    }
}
