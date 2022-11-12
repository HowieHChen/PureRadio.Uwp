using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.UI;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class RadioDetailViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IRadioProvider radioProvider;
        private readonly DispatcherTimer _refreshTimer;
        private PlaylistDay currentSource;

        private int _radioId;
        public int RadioId
        {
            get => _radioId;
            set
            {
                SetProperty(ref _radioId, value);
                GetRadioDetail();
                GetRadioPlaylist();
            }
        }

        [ObservableProperty]
        private string _title;
        [ObservableProperty]
        private BitmapImage _cover;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private string _audienceCount;
        [ObservableProperty]
        private string _nowplaying;
        [ObservableProperty]
        private string _topCategoryTitle;

        [ObservableProperty]
        private bool _isInfoLoading;

        [ObservableProperty]
        private bool _isPlaylistLoading;

        [ObservableProperty]
        private List<RadioPlaylistDetail> _radioPlaylist;

        private List<RadioPlaylistDetail> PlaylistsBYDay;
        private List<RadioPlaylistDetail> PlaylistsYDay;
        private List<RadioPlaylistDetail> PlaylistsToday;
        private List<RadioPlaylistDetail> PlaylistsTMR;

        public RadioDetailViewModel(
            INavigateService navigate, 
            IRadioProvider radioProvider)
        {
            this.navigate = navigate;
            this.radioProvider = radioProvider;
            IsActive = true;
            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.MaxValue,
            };
            _refreshTimer.Tick += OnRefreshTimerTick;
            Cover = new BitmapImage(new Uri("ms-appx:///Assets/Image/DefaultCover.png"));
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {

            base.OnDeactivated();
        }

        private async void GetRadioDetail()
        {
            if(RadioId != 0)
            {
                IsInfoLoading = true;
                var result = await radioProvider.GetRadioDetailInfo(RadioId, CancellationToken.None);
                Title = result.Title;
                Cover = await ImageCache.Instance.GetFromCacheAsync(result.Cover);
                Description = result.Description;
                AudienceCount = result.AudienceCount;
                Nowplaying = result.Nowplaying;
                TopCategoryTitle = result.TopCategoryTitle;
                if(result.UpdateTime != TimeSpan.Zero)
                {
                    if (_refreshTimer.IsEnabled) 
                        _refreshTimer.Stop();
                    _refreshTimer.Interval = result.UpdateTime.Add(TimeSpan.FromSeconds(1));
                    if (!_refreshTimer.IsEnabled)
                        _refreshTimer.Start();
                }
                else
                {
                    if (_refreshTimer.IsEnabled)
                        _refreshTimer.Stop();
                }
                IsInfoLoading = false;
            }
        }

        private async void GetRadioPlaylist()
        {
            if(RadioId != 0)
            {
                IsPlaylistLoading = true;
                var result = await radioProvider.GetRadioPlaylistDetail(RadioId, CancellationToken.None);
                if(result != null)
                {
                    PlaylistsBYDay = result?.BYday ?? new List<RadioPlaylistDetail>();
                    PlaylistsYDay = result?.Yday ?? new List<RadioPlaylistDetail>();
                    PlaylistsToday = result?.Today ?? new List<RadioPlaylistDetail>();
                    PlaylistsTMR = result?.Tmr ?? new List<RadioPlaylistDetail>();
                }
                else
                {
                    PlaylistsBYDay = new List<RadioPlaylistDetail>();
                    PlaylistsYDay = new List<RadioPlaylistDetail>();
                    PlaylistsToday = new List<RadioPlaylistDetail>();
                    PlaylistsTMR = new List<RadioPlaylistDetail>();
                }
                SwitchPlaylistSource(currentSource);
                IsPlaylistLoading = false;
            }
        }

        public void SwitchPlaylistSource(PlaylistDay target)
        {
            RadioPlaylist = target switch
            {
                PlaylistDay.BeforeYesterday => PlaylistsBYDay,
                PlaylistDay.Yesterday => PlaylistsYDay,
                PlaylistDay.Tomorrow => PlaylistsTMR,
                _ => PlaylistsToday
            };
            currentSource = target;
        }

        private void OnRefreshTimerTick(object sender, object e)
        {
            _refreshTimer.Stop();
            GetRadioDetail();
        }

    }
}
