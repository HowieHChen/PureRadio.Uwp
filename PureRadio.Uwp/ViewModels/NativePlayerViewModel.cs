using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.UI;
using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class NativePlayerViewModel : ObservableRecipient
    {
        private readonly IPlaybackService playService;
        private readonly DispatcherTimer _refreshTimer;

        private MediaPlayType _currentType;
        private TimeSpan _nowPositonTimeSpan;
        private DateTime _startDateTime;
        private int _ticksCount = 1;
        public bool IsMoveMediaPosition = false;
        public bool IsMoveVolume = false;

        [ObservableProperty]
        private bool _showElement;

        [ObservableProperty]
        private BitmapImage _cover;
        [ObservableProperty]
        private string _title;
        [ObservableProperty]
        private string _subTitle;
        [ObservableProperty]
        private string _nowPositonText;
        [ObservableProperty]
        private string _durationText;
        [ObservableProperty]
        private string _startTime;
        [ObservableProperty]
        private string _endTime;
        [ObservableProperty]
        private bool _isLive;

        [ObservableProperty]
        private bool _isMuted;
        [ObservableProperty]
        private double _volume;
        [ObservableProperty]
        private bool _canSkipPrevious;
        [ObservableProperty]
        private bool _canSkipNext;
        [ObservableProperty]
        private int _mediaTotalSeconds;
        [ObservableProperty]
        private int _mediaNowPosition;
        [ObservableProperty]
        private MediaPlaybackState _playerState;


        public NativePlayerViewModel(IPlaybackService playbackService)
        {
            playService = playbackService;
            UpdatePlayerState(playService.GetCurrentPlayerState());
            UpdatePlayerItem(playService.GetCurrentPlayItem());
            playService.PlayerStateChanged += PlayService_PlayerStateChanged;
            playService.PlayerItemChanged += PlayService_PlayerItemChanged;
            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),
            };
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            _refreshTimer.Tick += OnRefreshTimer_Tick;
        }

        protected override void OnDeactivated()
        {
            if (_refreshTimer.IsEnabled)
                _refreshTimer.Stop();
            _refreshTimer.Tick -= OnRefreshTimer_Tick;
            base.OnDeactivated();
        }

        private void OnRefreshTimer_Tick(object sender, object e)
        {
            _ticksCount++;
            if(_ticksCount > 5)
            {
                _ticksCount = 1;
                if (_currentType == MediaPlayType.RadioLive)
                {
                    if (!IsMoveMediaPosition) 
                        MediaNowPosition = (int)(DateTime.Now - _startDateTime).TotalSeconds;
                }
                else if (_currentType == MediaPlayType.RadioDemand || _currentType == MediaPlayType.ContentDemand)
                {
                    _nowPositonTimeSpan = playService.NowPosition;
                    if (!IsMoveMediaPosition) 
                        MediaNowPosition = (int)_nowPositonTimeSpan.TotalSeconds;
                    NowPositonText = _nowPositonTimeSpan.ToString(@"hh\:mm\:ss");
                }
                if(playService.AudioPlaybackState != MediaPlaybackState.Playing)
                {
                    _playerState = playService.AudioPlaybackState;
                    _refreshTimer.Stop();
                }
            }
            else
            {
                if (_currentType == MediaPlayType.RadioLive)
                {
                    if (!IsMoveMediaPosition) 
                        MediaNowPosition++;
                }
                else if (_currentType == MediaPlayType.RadioDemand || _currentType == MediaPlayType.ContentDemand)
                {
                    _nowPositonTimeSpan = _nowPositonTimeSpan.Add(TimeSpan.FromSeconds(1));
                    if (!IsMoveMediaPosition) 
                        MediaNowPosition = (int)_nowPositonTimeSpan.TotalSeconds;
                    NowPositonText = _nowPositonTimeSpan.ToString(@"hh\:mm\:ss");
                }
            }
        }

        private void PlayService_PlayerItemChanged(object sender, PlayerItemChangedEventArgs e)
        {
            UpdatePlayerItem(e.Snapshot);
        }

        private void PlayService_PlayerStateChanged(object sender, PlayerStateChangedEventArgs e)
        {
            UpdatePlayerState(e.Snapshot);
        }

        private async void UpdatePlayerState(PlayStateSnapshot playState)
        {
            if(playState != null)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PlayerState = playState.PlayerState;
                    CanSkipPrevious = playState.CanPrevious;
                    CanSkipNext = playState.CanNext;
                    IsMuted = playState.IsMuted;
                    if (!IsMoveVolume) Volume = playState.Volume;
                    if (_currentType != MediaPlayType.RadioLive)
                    {
                        _nowPositonTimeSpan = TimeSpan.FromSeconds(playState.NowPosition);
                        if (!IsMoveMediaPosition) 
                            MediaNowPosition = (int)_nowPositonTimeSpan.TotalSeconds;
                        MediaTotalSeconds = playState.TotalSeconds;
                        NowPositonText = _nowPositonTimeSpan.ToString(@"hh\:mm\:ss");
                        DurationText = TimeSpan.FromSeconds(playState.TotalSeconds).ToString(@"hh\:mm\:ss");
                    }
                    if ((PlayerState == MediaPlaybackState.Paused || PlayerState == MediaPlaybackState.None) && _refreshTimer.IsEnabled) 
                        _refreshTimer.Stop(); 
                    else if(!_refreshTimer.IsEnabled) 
                        _refreshTimer.Start();
                });                
            }
        }

        private async void UpdatePlayerItem(PlayItemSnapshot playItem)
        {
            if(playItem != null)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    _currentType = playItem.Type;
                    if (playItem.Type == MediaPlayType.None)
                    {
                        var cover = new BitmapImage(new Uri("ms-appx:///Assets/Image/DefaultCover.png"));
                        cover.DecodePixelHeight = cover.DecodePixelWidth = 60;
                        Cover = cover;
                        Title = SubTitle = NowPositonText = DurationText = StartTime = EndTime = string.Empty;
                        ShowElement = IsLive = false;
                        MediaTotalSeconds = MediaNowPosition = 0;
                    }
                    else
                    {
                        Cover = (await ImageCache.Instance.GetFromCacheAsync(playItem.Cover)) ?? new BitmapImage(new Uri("ms-appx:///Assets/Image/DefaultCover.png"));
                        Cover.DecodePixelHeight = Cover.DecodePixelWidth = 60;
                        Cover.DecodePixelType = DecodePixelType.Logical;
                        Title = playItem.Title;
                        SubTitle = playItem.SubTitle;
                        if (_currentType == MediaPlayType.RadioLive)
                        {
                            DurationText = TimeSpan.FromSeconds(playItem.Duration).ToString(@"hh\:mm\:ss");
                            MediaTotalSeconds = playItem.Duration;
                            StartTime = playItem.StartTime;
                            EndTime = playItem.EndTime;
                            if (DateTime.TryParse(StartTime, out _startDateTime))
                            {
                                if (!IsMoveMediaPosition) MediaNowPosition = (int)(DateTime.Now - _startDateTime).TotalSeconds;
                            }
                        }
                        ShowElement = playItem.Type != MediaPlayType.None;
                        IsLive = playItem.Type == MediaPlayType.RadioLive;
                    }                   
                });
            }            
        }

        public void TogglePlay()
        {
            switch (PlayerState)
            {
                default:
                case MediaPlaybackState.None:
                case MediaPlaybackState.Opening:
                    break;
                case MediaPlaybackState.Buffering:
                    playService.Pause();
                    break;
                case MediaPlaybackState.Playing:
                    playService.Pause();
                    break;
                case MediaPlaybackState.Paused:
                    playService.Play();
                    break;
            }
        }

        public void TryPrevious()
        {
            if (CanSkipPrevious) playService.Previous();
        }

        public void TryNext()
        {
            if (CanSkipNext) playService.Next();
        }

        public void Mute()
        {
            playService.SetMute(!IsMuted);
        }

        public void SetVolume(double volume)
        {
            playService.SetVolume(volume);
        }

        public void SetPosition(int position)
        {
            if(_currentType == MediaPlayType.RadioDemand || _currentType == MediaPlayType.ContentDemand)
                playService.SetPosition(position);
        }

        public void Refresh()
        {
            if(_currentType != MediaPlayType.None)
                playService.Refresh();
        }
    }
}
