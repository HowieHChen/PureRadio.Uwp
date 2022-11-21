using CommunityToolkit.Mvvm.ComponentModel;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class RadioViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IRadioProvider radioProvider;
        private readonly ISettingsService settings;
        private readonly DispatcherTimer _refreshTimer;

        [ObservableProperty]
        private bool _isRecLoading;
        [ObservableProperty]
        private bool _isRecEmpty;
        [ObservableProperty]
        private List<RadioInfoRecommend> _listRecommend;

        [ObservableProperty]
        private bool _isNetTrendLoading;
        [ObservableProperty]
        private bool _isNetTrendEmpty;
        [ObservableProperty]
        private List<RadioInfoSummary> _listNetTrend;

        [ObservableProperty]
        private bool _isLocalTrendLoading;
        [ObservableProperty]
        private bool _isLocalTrendEmpty;
        [ObservableProperty]
        private List<RadioInfoSummary> _listLocalTrend;

        [ObservableProperty]
        private bool _isCategoryLoading;
        [ObservableProperty]
        private List<RadioCategoryItem> _listRadioCategory;

        [ObservableProperty]
        private int _provinceId;

        public RadioViewModel(
            INavigateService navigate, 
            IRadioProvider radioProvider,
            ISettingsService settings)
        {
            this.navigate = navigate;
            this.radioProvider = radioProvider;
            this.settings = settings;
            ListRecommend = new List<RadioInfoRecommend>();
            ListNetTrend = new List<RadioInfoSummary>();
            ListLocalTrend = new List<RadioInfoSummary>();
            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(3),
            };
            GetRadioData();
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
                GetRadioData();
            }
        }

        public async void GetRadioData()
        {
            IsRecLoading = true;
            IsNetTrendLoading = true;
            IsLocalTrendLoading = true;
            IsCategoryLoading = true;
            ListRecommend.Clear();
            ListRecommend = await radioProvider.GetRadioRecommendResult(CancellationToken.None);
            IsRecEmpty = ListRecommend.Count == 0;
            IsRecLoading = false;

            ListNetTrend.Clear();
            ListNetTrend = await radioProvider.GetRadioBillboardResult(407, CancellationToken.None);
            IsNetTrendEmpty = ListNetTrend.Count == 0;
            IsNetTrendLoading = false;

            ListLocalTrend.Clear();
            ProvinceId = settings.GetValue<int?>(AppConstants.SettingsKey.LocalRegionId) ?? 0;
            ListLocalTrend = await radioProvider.GetRadioBillboardResult(ProvinceId, CancellationToken.None);
            IsLocalTrendEmpty = ListLocalTrend.Count == 0;
            IsLocalTrendLoading = false;

            ListRadioCategory = AppConstants.RadioCategories;
            IsCategoryLoading = false;
        }

        public void Navigate(PageIds pageId, object parameter = null)
        {
            if (pageId == PageIds.RadioDetail || pageId == PageIds.RadioCategory)
            {
                navigate.NavigateToSecondaryView(pageId, new EntranceNavigationTransitionInfo(), parameter);
            }
        }
    }
}
