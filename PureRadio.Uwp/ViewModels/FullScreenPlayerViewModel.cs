using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI;
using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Services;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class FullScreenPlayerViewModel : ObservableRecipient
    {
        private readonly IPlaybackService playService;
        private readonly INavigateService navigate;
        private readonly ILibraryService library;
        private readonly DispatcherTimer _refreshTimer;

        public event EventHandler<MediaPlaybackState> PlayerStateChanged;

        private MediaPlayType _currentType;
        private TimeSpan _nowPositonTimeSpan;
        private DateTime _startDateTime;
        private int _ticksCount = 1;
        private PlayItemSnapshot itemSnapshot;
        public bool IsMoveMediaPosition = false;
        public bool IsMoveVolume = false;

        public IAsyncRelayCommand ToggleFavCommand { get; }

        [ObservableProperty]
        private bool _isFav;

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
        private string _nowTime;
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

        [ObservableProperty]
        private bool _isPlaylistLoading;
        [ObservableProperty]
        private bool _isPlaylistShown;
        [ObservableProperty]
        private List<PlayItemSnapshot> _playlist;

        public FullScreenPlayerViewModel(
            IPlaybackService playbackService,
            ILibraryService libraryService,
            INavigateService navigateService)
        {
            playService = playbackService;
            library = libraryService;
            navigate = navigateService;
            UpdatePlayerState(playService.GetCurrentPlayerState());
            itemSnapshot = playService.GetCurrentPlayItem();
            UpdatePlayerItem(itemSnapshot);
            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),
            };
            Cover = new BitmapImage(new Uri("ms-appx:///Assets/Image/DefaultCover.png"));
            ToggleFavCommand = new AsyncRelayCommand(ToggleFavState);
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            _refreshTimer.Tick += OnRefreshTimer_Tick;
            playService.PlayerStateChanged += PlayService_PlayerStateChanged;
            playService.PlayerItemChanged += PlayService_PlayerItemChanged;
            library.FavItemChanging += Library_FavItemChanging;
        }

        protected override void OnDeactivated()
        {
            if (_refreshTimer.IsEnabled)
                _refreshTimer.Stop();
            _refreshTimer.Tick -= OnRefreshTimer_Tick;
            playService.PlayerStateChanged -= PlayService_PlayerStateChanged;
            playService.PlayerItemChanged -= PlayService_PlayerItemChanged;
            library.FavItemChanging -= Library_FavItemChanging;
            base.OnDeactivated();
        }

        private void Library_FavItemChanging(object sender, FavItemChangedEventArgs e)
        {
            bool isTypeMatched = _currentType switch
            {
                MediaPlayType.RadioLive => e.ItemType == MediaPlayType.RadioLive,
                MediaPlayType.RadioDemand => e.ItemType == MediaPlayType.RadioLive,
                MediaPlayType.ContentDemand => e.ItemType == MediaPlayType.ContentDemand,
                _ => false,
            };
            if (isTypeMatched && e.MainId == itemSnapshot.MainId)
            {
                IsFav = e.Action switch
                {
                    LibraryItemAction.Add => true,
                    LibraryItemAction.Remove => false,
                    LibraryItemAction.Update => true,
                    _ => false,
                };
            }
        }

        private void OnRefreshTimer_Tick(object sender, object e)
        {
            _ticksCount++;
            if (_ticksCount > 5)
            {
                _ticksCount = 1;
                if (_currentType == MediaPlayType.RadioLive)
                {
                    if (!IsMoveMediaPosition)
                        MediaNowPosition = (int)(DateTime.Now - _startDateTime).TotalSeconds;
                    NowTime = DateTime.Now.ToString(@"HH\:mm\:ss");
                }
                else if (_currentType == MediaPlayType.RadioDemand || _currentType == MediaPlayType.ContentDemand)
                {
                    _nowPositonTimeSpan = playService.NowPosition;
                    if (!IsMoveMediaPosition)
                        MediaNowPosition = (int)_nowPositonTimeSpan.TotalSeconds;
                    NowPositonText = _nowPositonTimeSpan.ToString(@"hh\:mm\:ss");
                }
                if (playService.AudioPlaybackState != MediaPlaybackState.Playing)
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
                    NowTime = DateTime.Now.ToString(@"HH\:mm\:ss");
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
            if (playState != null)
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
                    if (PlayerState == MediaPlaybackState.Paused || PlayerState == MediaPlaybackState.None)
                    {
                        if (_refreshTimer.IsEnabled)
                            _refreshTimer.Stop();
                    }
                    else if (!_refreshTimer.IsEnabled)
                        _refreshTimer.Start();
                    PlayerStateChanged?.Invoke(this, PlayerState);
                });
            }
        }

        private async void UpdatePlayerItem(PlayItemSnapshot playItem)
        {
            if (playItem != null)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    _currentType = playItem.Type;
                    if (playItem.Type == MediaPlayType.None)
                    {
                        var cover = new BitmapImage(new Uri("ms-appx:///Assets/Image/DefaultCover.png"));
                        cover.DecodePixelHeight = Cover.DecodePixelWidth = 400;
                        cover.DecodePixelType = DecodePixelType.Logical;
                        Cover = cover;
                        Title = SubTitle = NowPositonText = DurationText = StartTime = EndTime = string.Empty;
                        ShowElement = IsLive = false;
                        MediaTotalSeconds = MediaNowPosition = 0;
                        IsFav = false;
                    }
                    else
                    {
                        var cover = (await ImageCache.Instance.GetFromCacheAsync(playItem.Cover)) ?? new BitmapImage(new Uri("ms-appx:///Assets/Image/DefaultCover.png"));
                        cover.DecodePixelHeight = Cover.DecodePixelWidth = 400;
                        cover.DecodePixelType = DecodePixelType.Logical;
                        Cover = cover;
                        Title = playItem.Title;
                        SubTitle = playItem.SubTitle;
                        if (_currentType == MediaPlayType.RadioLive)
                        {
                            DurationText = TimeSpan.FromSeconds(playItem.Duration).ToString(@"hh\:mm\:ss");
                            MediaTotalSeconds = playItem.Duration;
                            StartTime = playItem.StartTime;
                            NowTime = DateTime.Now.ToString(@"HH\:mm\:ss");
                            EndTime = playItem.EndTime;
                            if (DateTime.TryParse(StartTime, out _startDateTime))
                            {
                                if (!IsMoveMediaPosition) MediaNowPosition = (int)(DateTime.Now - _startDateTime).TotalSeconds;
                            }
                        }
                        else
                        {
                            Playlist = playService.GetCurrentPlayList();
                        }
                        ShowElement = playItem.Type != MediaPlayType.None;
                        IsLive = playItem.Type == MediaPlayType.RadioLive;
                        MediaPlayType mediaType = _currentType == MediaPlayType.RadioDemand ? MediaPlayType.RadioLive : _currentType;
                        IsFav = await library.IsFavItem(mediaType, playItem.MainId);
                    }
                    itemSnapshot = playItem;
                });
            }
        }

        public async Task ToggleFavState()
        {
            MediaPlayType mediaType = _currentType == MediaPlayType.RadioDemand ? MediaPlayType.RadioLive : _currentType;
            if (mediaType != MediaPlayType.None && itemSnapshot != null)
            {
                if (IsFav)
                {
                    _ = await library.RemoveFromFav(mediaType, itemSnapshot.MainId);
                }
                else
                {
                    _ = await library.AddToFav(itemSnapshot);
                }
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
            if (_currentType != MediaPlayType.None)
                if (CanSkipPrevious) playService.Previous();
        }

        public void TryNext()
        {
            if (_currentType != MediaPlayType.None)
                if (CanSkipNext) playService.Next();
        }

        public void Mute()
        {
            if (_currentType != MediaPlayType.None)
                playService.SetMute(!IsMuted);
        }

        public void SetVolume(double volume)
        {
            if (_currentType != MediaPlayType.None)
                playService.SetVolume(volume);
        }

        public void SetPosition(int position)
        {
            if (_currentType == MediaPlayType.RadioDemand || _currentType == MediaPlayType.ContentDemand)
                playService.SetPosition(position);
        }

        public void NavigateDetail()
        {
            switch (_currentType)
            {
                default:
                case MediaPlayType.None:
                    break;
                case MediaPlayType.RadioLive:
                case MediaPlayType.RadioDemand:
                    navigate.NavigateToSecondaryView(PageIds.RadioDetail, new EntranceNavigationTransitionInfo(), itemSnapshot.MainId);
                    break;
                case MediaPlayType.ContentDemand:
                    navigate.NavigateToSecondaryView(PageIds.ContentDetail, new EntranceNavigationTransitionInfo(), itemSnapshot.MainId);
                    break;
            }
        }

        public void NavigateBack()
        {
            navigate.NavigateToPlayView(new EntranceNavigationTransitionInfo(), true);
        }

        public void PlayItem(PlayItemSnapshot item, int index)
        {
            if(item.Type == MediaPlayType.RadioDemand)
            {
                playService.PlayRadioDemand(item.MainId, index, Playlist);
            }
            else if(item.Type == MediaPlayType.ContentDemand)
            {
                playService.PlayContent(item.MainId, item.SecondaryId, Playlist);
            }
        }
    }
}
