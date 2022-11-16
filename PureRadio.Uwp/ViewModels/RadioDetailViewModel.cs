using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.UI;
using PureRadio.Uwp.Adapters;
using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Providers;
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
using Windows.ApplicationModel.Core;
using Windows.Media.Editing;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class RadioDetailViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IPlaybackService playbackService;
        private readonly IRadioProvider radioProvider;
        private readonly IPlayerAdapter playerAdapter;
        private readonly DispatcherTimer _refreshTimer;
        private PlaylistDay currentSource;
        private PlayItemSnapshot itemSnapshot;
        private RadioInfoDetail radioDetail;

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
        private bool _isShowErrorTips;

        [ObservableProperty]
        private List<RadioPlaylistDetail> _radioPlaylist;

        private List<RadioPlaylistDetail> PlaylistsBYDay;
        private List<RadioPlaylistDetail> PlaylistsYDay;
        private List<RadioPlaylistDetail> PlaylistsToday;
        private List<RadioPlaylistDetail> PlaylistsTMR;

        public RadioDetailViewModel(
            INavigateService navigate, 
            IRadioProvider radioProvider,
            IPlaybackService playbackService,
            IPlayerAdapter playerAdapter)
        {
            this.navigate = navigate;
            this.playbackService = playbackService;
            this.radioProvider = radioProvider;
            this.playerAdapter = playerAdapter;
            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.MaxValue,
            };
            Cover = new BitmapImage(new Uri("ms-appx:///Assets/Image/DefaultCover.png"));
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            _refreshTimer.Tick += OnRefreshTimerTick;
        }

        protected override void OnDeactivated()
        {
            if (_refreshTimer.IsEnabled)
                _refreshTimer.Stop();
            _refreshTimer.Tick -= OnRefreshTimerTick;
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
                Cover.DecodePixelHeight = Cover.DecodePixelWidth = 200;
                Cover.DecodePixelType = DecodePixelType.Logical;
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
                itemSnapshot = playerAdapter.ConvertToPlayItemSnapshot(result);
                radioDetail = result;
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
            UpdateRadioLiveInfo();
        }

        public void PlayRadioLive()
        {
            if(itemSnapshot.Duration != 0)
            {
                playbackService.PlayRadioLive(RadioId, itemSnapshot);
            }
        }

        public void PlayRadioDemand(int index)
        {
            if (RadioId != 0 && index >= 0 && index < RadioPlaylist.Count())
                switch (currentSource)
                {
                    case PlaylistDay.Tomorrow:
                        IsShowErrorTips = true;
                        break;
                    case PlaylistDay.Today:
                        if (DateTime.TryParse(RadioPlaylist[index].EndTime, out DateTime _endDateTime))
                        {
                            if(DateTime.Now > _endDateTime)
                            {
                                var playList = playerAdapter.ConvertToPlayItemSnapshotList(radioDetail, RadioPlaylist);
                                playbackService.PlayRadioDemand(RadioId, index, playList);
                            }
                            else
                            {
                                IsShowErrorTips = true;
                            }
                        }
                        break;
                    default:
                        var snapshotList = playerAdapter.ConvertToPlayItemSnapshotList(radioDetail, RadioPlaylist);
                        playbackService.PlayRadioDemand(RadioId, index, snapshotList);
                        break;
                }
        }

        private async void UpdateRadioLiveInfo()
        {
            var detail = await radioProvider.GetRadioDetailInfo(RadioId, CancellationToken.None);
            int count = 0;
            while (detail.RadioId == RadioId && detail.EndTime == radioDetail.EndTime && detail.UpdateTime != TimeSpan.Zero)
            {
                if (count < 5)
                    await Task.Run(() =>
                    {
                        Thread.Sleep(1500);
                    });
                else
                    await Task.Run(() =>
                    {
                        Thread.Sleep(3000);
                    });
                detail = await radioProvider.GetRadioDetailInfo(RadioId, CancellationToken.None);
                count++;
            }
            IsInfoLoading = true;
            Title = detail.Title;
            Cover = await ImageCache.Instance.GetFromCacheAsync(detail.Cover);
            Cover.DecodePixelHeight = Cover.DecodePixelWidth = 200;
            Cover.DecodePixelType = DecodePixelType.Logical;
            Description = detail.Description;
            AudienceCount = detail.AudienceCount;
            Nowplaying = detail.Nowplaying;
            TopCategoryTitle = detail.TopCategoryTitle;
            if (detail.UpdateTime != TimeSpan.Zero)
            {
                if (_refreshTimer.IsEnabled)
                    _refreshTimer.Stop();
                _refreshTimer.Interval = detail.UpdateTime.Add(TimeSpan.FromSeconds(1));
                if (!_refreshTimer.IsEnabled)
                    _refreshTimer.Start();
            }
            else
            {
                if (_refreshTimer.IsEnabled)
                    _refreshTimer.Stop();
            }
            itemSnapshot = playerAdapter.ConvertToPlayItemSnapshot(detail);
            radioDetail = detail;
            IsInfoLoading = false;
            if (DateTime.Now.Hour == 0 && DateTime.Now.Second < 5) GetRadioPlaylist();
        }
    }
}
