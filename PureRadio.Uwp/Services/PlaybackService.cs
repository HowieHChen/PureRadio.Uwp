using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.Streaming.Adaptive;
using Windows.Storage.Streams;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Web.Http;

namespace PureRadio.Uwp.Services
{
    public class PlaybackService : IPlaybackService
    {
        private readonly ISettingsService _settingsService;
        private readonly IPlayerAdapter _playerAdapter;
        private readonly IRadioProvider _radioProvider;
        private readonly IContentProvider _contentProvider;
        private readonly IAccountProvider _accountProvider;
        private readonly System.Timers.Timer _refreshTimer;

        public MediaPlayer AudioPlayer { get; set; }

        public TimeSpan NowPosition => AudioPlayer.PlaybackSession.Position;

        public TimeSpan NaturalDuration => AudioPlayer.PlaybackSession.NaturalDuration;

        public MediaPlaybackState AudioPlaybackState => AudioPlayer.PlaybackSession.PlaybackState;

        private MediaSource _mediaSource;
        private AdaptiveMediaSource _adaptiveMediaSource;
        private MediaPlaybackItem _mediaPlaybackItem;
        private MediaPlaybackList _mediaPlaybackList;
        private HttpClient _liveStream;
        private SystemMediaTransportControls _transportControls;

        private MediaPlaybackState _nowState;
        private MediaPlayType _currentType;

        public event EventHandler<PlayerItemChangedEventArgs> PlayerItemChanged;
        public event EventHandler<PlayerStateChangedEventArgs> PlayerStateChanged;

        private PlayItemSnapshot _playItem;
        private List<PlayItemSnapshot> _playList;
        private uint _radioPlayListIndex;
        private int _contentPlayListIndex;

        private bool _canPrevious;
        private bool _canNext;


        public PlaybackService(
            ISettingsService settingsService,
            IPlayerAdapter playerAdapter,
            IRadioProvider radioProvider,
            IContentProvider contentProvider,
            IAccountProvider accountProvider)
        {
            _settingsService = settingsService;
            _playerAdapter = playerAdapter;
            _radioProvider = radioProvider;
            _contentProvider = contentProvider;
            _accountProvider = accountProvider;
            AudioPlayer = new MediaPlayer()
            {
                AutoPlay = false
            };
            AudioPlayer.SourceChanged += AudioPlayer_SourceChanged;
            AudioPlayer.MediaOpened += AudioPlayer_MediaOpened;
            AudioPlayer.MediaEnded += AudioPlayer_MediaEnded;
            AudioPlayer.MediaFailed += AudioPlayer_MediaFailed;
            AudioPlayer.PlaybackSession.PlaybackStateChanged += PlaybackSession_PlaybackStateChanged;
            AudioPlayer.Volume = _settingsService.GetValue<double?>(AppConstants.SettingsKey.SavedVolumeState) ?? 1;
            InitTransportControls();
            _liveStream = new();
            _liveStream.DefaultRequestHeaders.TryAppendWithoutValidation("sec-fetch-dest", "audio");
            _liveStream.DefaultRequestHeaders.TryAppendWithoutValidation("sec-fetch-mode", "no-cors");
            _liveStream.DefaultRequestHeaders.TryAppendWithoutValidation("sec-fetch-site", "cross-site");
            _refreshTimer = new System.Timers.Timer()
            {
                Interval = double.MaxValue,
            };
            _refreshTimer.Elapsed += OnRefreshTimer_Tick;
            _playItem = new PlayItemSnapshot(MediaPlayType.None, null, new Uri("ms-appx:///Assets/Image/DefaultCover.png"), string.Empty, string.Empty, 0, 0, 0);
            _nowState = MediaPlaybackState.None;
        }

        private void InitTransportControls()
        {
            _transportControls = SystemMediaTransportControls.GetForCurrentView();
            AudioPlayer.CommandManager.IsEnabled = false;
            _transportControls.IsPlayEnabled = true;
            _transportControls.IsPauseEnabled = true;
            _transportControls.ButtonPressed += CustomTransportControls_ButtonPressed;
        }

        private void CustomTransportControls_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    Play();
                    break;
                case SystemMediaTransportControlsButton.Pause:
                    Pause();
                    break;
                case SystemMediaTransportControlsButton.Previous:
                    Previous();
                    break;
                case SystemMediaTransportControlsButton.Next:
                    Next();
                    break;
                default:
                    break;
            }
        }

        private void PlaybackSession_PlaybackStateChanged(MediaPlaybackSession sender, object args)
        {
            if (sender.PlaybackState != _nowState)
            {
                _nowState = sender.PlaybackState;
                PlayerStateChanged?.Invoke(this, new PlayerStateChangedEventArgs(
                GetCurrentPlayerState()));
            }
            _transportControls.PlaybackStatus = sender.PlaybackState switch
            {
                MediaPlaybackState.Playing => MediaPlaybackStatus.Playing,
                MediaPlaybackState.Paused => MediaPlaybackStatus.Paused,
                _ => MediaPlaybackStatus.Closed,
            };
        }

        private void OnRefreshTimer_Tick(object sender, object e)
        {
            _refreshTimer.Stop();
            UpdateRadioLiveInfo();
        }

        private void AudioPlayer_MediaFailed(MediaPlayer sender, MediaPlayerFailedEventArgs args)
        {
            //throw new NotImplementedException();
        }

        private void AudioPlayer_MediaEnded(MediaPlayer sender, object args)
        {
            PlayerStateChanged?.Invoke(this, new PlayerStateChangedEventArgs(
                    GetCurrentPlayerState()));
            _playItem = new PlayItemSnapshot(MediaPlayType.None, null, new Uri("ms-appx:///Assets/Image/DefaultCover.png"), string.Empty, string.Empty, 0, 0, 0);
            PlayerItemChanged?.Invoke(this, new PlayerItemChangedEventArgs(
                _playItem));
        }

        private void AudioPlayer_MediaOpened(MediaPlayer sender, object args)
        {
            //PlayerStateChanged?.Invoke(this, new PlayerStateChangedEventArgs(
            //        GetCurrentPlayerState()));
            PlayerItemChanged?.Invoke(this, new PlayerItemChangedEventArgs(
                _playItem));
        }

        private void AudioPlayer_SourceChanged(MediaPlayer sender, object args)
        {
            if (_currentType != MediaPlayType.RadioLive && _refreshTimer.Enabled)
                _refreshTimer.Stop();
        }

        private void MediaPlaybackList_CurrentItemChanged(MediaPlaybackList sender, CurrentMediaPlaybackItemChangedEventArgs args)
        {
            if (args.NewItem == null) return;
            int index = sender.Items.IndexOf(args.NewItem);
            _radioPlayListIndex = Convert.ToUInt32(index);
            _playItem = _playList[index];
            UpdateProperties(_playItem);
            PlayerItemChanged?.Invoke(this, new PlayerItemChangedEventArgs(
                _playItem));
        }

        private void UpdateProperties(PlayItemSnapshot playItem)
        {
            var resources = new ResourceLoader();
            //MediaItemDisplayProperties props = _mediaPlaybackItem.GetDisplayProperties();
            //props.Type = Windows.Media.MediaPlaybackType.Music;
            //props.MusicProperties.Title = _currentType == MediaPlayType.RadioLive ? resources.GetString("LangLiveNow") : playItem.Title;
            //props.MusicProperties.Artist = playItem.SubTitle;
            //props.Thumbnail = RandomAccessStreamReference.CreateFromUri(playItem.Cover);
            //_mediaPlaybackItem.ApplyDisplayProperties(props);

            // Get the updater.
            SystemMediaTransportControlsDisplayUpdater updater = _transportControls.DisplayUpdater;

            // Music metadata.
            updater.Type = MediaPlaybackType.Music;
            updater.MusicProperties.Title = playItem.Title;
            updater.MusicProperties.Artist = playItem.SubTitle;

            // Set the album art thumbnail.
            // RandomAccessStreamReference is defined in Windows.Storage.Streams
            updater.Thumbnail = RandomAccessStreamReference.CreateFromUri(playItem.Cover);

            // Update the system media transport controls.
            updater.Update();

        }

        public async void PlayRadioLive(int radioId)
        {
            var detail = await _radioProvider.GetRadioDetailInfo(radioId, CancellationToken.None);
            PlayRadioLive(radioId, _playerAdapter.ConvertToPlayItemSnapshot(detail));
        }

        public async void PlayRadioLive(int radioId, PlayItemSnapshot playItem)
        {
            var detail = await _radioProvider.GetRadioDetailInfo(radioId, CancellationToken.None);
            AdaptiveMediaSourceCreationResult result = await AdaptiveMediaSource.CreateFromUriAsync(playItem.SourceUri, _liveStream);
            if (result.Status == AdaptiveMediaSourceCreationStatus.Success)
            {
                if (AudioPlayer.PlaybackSession.PlaybackState != MediaPlaybackState.None) Pause();
                _adaptiveMediaSource = result.MediaSource;
                _mediaSource = MediaSource.CreateFromAdaptiveMediaSource(_adaptiveMediaSource);
                _mediaPlaybackItem = new MediaPlaybackItem(_mediaSource);
                AudioPlayer.Source = _mediaPlaybackItem;
                _adaptiveMediaSource.InitialBitrate = _adaptiveMediaSource.AvailableBitrates.Max<uint>();
                _currentType = MediaPlayType.RadioLive;
                _playItem = playItem;
                _canPrevious = _canNext = false;
                //UpdateProperties(playItem);
                //PlayerItemChanged?.Invoke(this, new PlayerItemChangedEventArgs(
                //    _playItem));
                _transportControls.IsPreviousEnabled = _transportControls.IsNextEnabled = false;
                Play();
                UpdateProperties(playItem);
                if (detail.UpdateTime != TimeSpan.Zero)
                {
                    if (_refreshTimer.Enabled)
                        _refreshTimer.Stop();
                    _refreshTimer.Interval = detail.UpdateTime.Add(TimeSpan.FromSeconds(1)).TotalMilliseconds;
                    if (!_refreshTimer.Enabled)
                        _refreshTimer.Start();
                }
                else
                {
                    if (_refreshTimer.Enabled)
                        _refreshTimer.Stop();
                }
                //Register for download requests
                //ams.DownloadRequested += DownloadRequested;

                //Register for download failure and completion events
                //ams.DownloadCompleted += DownloadCompleted;
                //ams.DownloadFailed += DownloadFailed;

                //Register for bitrate change events
                //ams.DownloadBitrateChanged += DownloadBitrateChanged;
                //ams.PlaybackBitrateChanged += PlaybackBitrateChanged;

                //Register for diagnostic event
                //ams.Diagnostics.DiagnosticAvailable += DiagnosticAvailable;
            }
            else
            {
                //Debug.Print("InitializeAdaptiveMediaSource Failed");
            }
        }

        public async void PlayRadioDemand(int radioId, int index, PlaylistDay day)
        {
            var detail = await _radioProvider.GetRadioDetailInfo(radioId, CancellationToken.None);
            var radioPlaylist = await _radioProvider.GetRadioPlaylistDetail(radioId, day, CancellationToken.None);
            var playlist = _playerAdapter.ConvertToPlayItemSnapshotList(detail, radioPlaylist);
            PlayRadioDemand(radioId, index, playlist);
        }

        public async void PlayRadioDemand(int radioId, int index, List<PlayItemSnapshot> radioPlaylist)
        {
            _radioPlayListIndex = Convert.ToUInt32(index);
            if(_currentType == MediaPlayType.RadioDemand && _playItem != null && _playItem.MainId == radioId && _playItem.DayOfWeek == radioPlaylist[index].DayOfWeek)
            {
                (AudioPlayer.Source as MediaPlaybackList).MoveTo(_radioPlayListIndex);
                Play();
                return;
            }
            if (_mediaPlaybackList != null) _mediaPlaybackList.CurrentItemChanged -= MediaPlaybackList_CurrentItemChanged;
            _mediaPlaybackList = new MediaPlaybackList();
            await Task.Run(() =>
            {
                var list = radioPlaylist.Select(p => _playerAdapter.ConvertToMediaPlaybackItem(p)).ToList();
                foreach (var item in list) _mediaPlaybackList.Items.Add(item);
            });
            if (_mediaPlaybackList.Items.Count > 0)
            {
                _currentType = MediaPlayType.RadioDemand;
                _playList = radioPlaylist;
                _canPrevious = _canNext = true;
                //_canPrevious = _radioPlayListIndex > 0;
                //_canNext = _radioPlayListIndex < _playList.Count - 1;
                AudioPlayer.Source = _mediaPlaybackList;
                _mediaPlaybackList.CurrentItemChanged += MediaPlaybackList_CurrentItemChanged;
                _mediaPlaybackList.MoveTo(_radioPlayListIndex);
                _transportControls.IsPreviousEnabled = _canPrevious;
                _transportControls.IsNextEnabled = _canNext;
                Play();
            }
        }

        public async void PlayContent(int contentId, int programId, string version)
        {
            if (_currentType == MediaPlayType.ContentDemand && _playItem != null && _playList != null && _playItem.MainId == contentId)
            {
                PlayContent(contentId, programId, _playList);
                return;
            }
            var detail = await _contentProvider.GetContentDetailInfo(contentId, CancellationToken.None);
            var contentPlaylist = await _contentProvider.GetContentProgramListFull(contentId, version, CancellationToken.None);
            var playlist = _playerAdapter.ConvertToPlayItemSnapshotList(detail, contentPlaylist);
            PlayContent(contentId, programId, playlist);
        }

        public async void PlayContent(int contentId, int programId, List<PlayItemSnapshot> contentPlaylist)
        {
            PlayItemSnapshot selectedItem = contentPlaylist.FirstOrDefault(p => p.SecondaryId == programId);
            if (selectedItem != null)
            {
                _currentType = MediaPlayType.ContentDemand;
                _playList = contentPlaylist;
                _contentPlayListIndex = contentPlaylist.IndexOf(selectedItem);
                await PlayContentDemand(selectedItem);
            }
        }

        public PlayStateSnapshot GetCurrentPlayerState()
        {
            double volume = AudioPlayer.Volume * 100;
            int totalSeconds = (int)AudioPlayer.PlaybackSession.NaturalDuration.TotalSeconds;
            int nowPosition = (int)AudioPlayer.PlaybackSession.Position.TotalSeconds;
            return new PlayStateSnapshot(
                AudioPlayer.PlaybackSession.PlaybackState, _canPrevious, _canNext, AudioPlayer.IsMuted, volume, totalSeconds, nowPosition);
        }

        public PlayItemSnapshot GetCurrentPlayItem()
        {
            return _playItem;
        }

        public List<PlayItemSnapshot> GetCurrentPlayList()
        {
            return _playList;
        }

        public void Play()
        {
            AudioPlayer.Play();
            //PlayerStateChanged?.Invoke(this, new PlayerStateChangedEventArgs(
            //    GetCurrentPlayerState()));
        }

        public void Pause()
        {
            AudioPlayer.Pause();
            //PlayerStateChanged?.Invoke(this, new PlayerStateChangedEventArgs(
            //    GetCurrentPlayerState()));
        }

        public async void Previous()
        {
            if (_canPrevious)
            {
                switch (_currentType)
                {
                    case MediaPlayType.RadioDemand:
                        _radioPlayListIndex--;
                        _mediaPlaybackList.MovePrevious();
                        //_canPrevious = _radioPlayListIndex > 0;
                        //_canNext = _radioPlayListIndex < _playList.Count;
                        break;
                    case MediaPlayType.ContentDemand:
                        _contentPlayListIndex--;
                        await PlayContentDemand(_playList[_contentPlayListIndex]);
                        break;
                    default:
                        _canPrevious = false;
                        break;
                }
                PlayerStateChanged?.Invoke(this, new PlayerStateChangedEventArgs(
                    GetCurrentPlayerState()));
            }
        }

        public async void Next()
        {
            if (_canNext)
            {
                switch (_currentType)
                {
                    case MediaPlayType.RadioDemand:
                        _radioPlayListIndex++;
                        _mediaPlaybackList.MoveNext();
                        //_canPrevious = _radioPlayListIndex > 0;
                        //_canNext = _radioPlayListIndex < _playList.Count;
                        break;
                    case MediaPlayType.ContentDemand:
                        _contentPlayListIndex++;
                        await PlayContentDemand(_playList[_contentPlayListIndex]);
                        break;
                    default:
                        _canNext = false;
                        break;
                }
                PlayerStateChanged?.Invoke(this, new PlayerStateChangedEventArgs(
                    GetCurrentPlayerState()));
            }
        }

        public void SetMute(bool muted)
        {
            AudioPlayer.IsMuted = muted;
            PlayerStateChanged?.Invoke(this, new PlayerStateChangedEventArgs(
                GetCurrentPlayerState()));
        }

        public void SetVolume(double volume)
        {
            if (!AudioPlayer.IsMuted)
            {
                AudioPlayer.Volume = volume / 100;
                _settingsService.SetValue<double>(AppConstants.SettingsKey.SavedVolumeState, AudioPlayer.Volume);
                PlayerStateChanged?.Invoke(this, new PlayerStateChangedEventArgs(
                    GetCurrentPlayerState()));
            }
        }

        public void SetPosition(int position)
        {
            AudioPlayer.PlaybackSession.Position = TimeSpan.FromSeconds(position);
            PlayerStateChanged?.Invoke(this, new PlayerStateChangedEventArgs(
                GetCurrentPlayerState()));
        }

        private async void UpdateRadioLiveInfo()
        {
            var detail = await _radioProvider.GetRadioDetailInfo(_playItem.MainId, CancellationToken.None);
            int count = 0;
            while (detail.RadioId == _playItem.MainId && detail.EndTime == _playItem.EndTime && detail.UpdateTime != TimeSpan.Zero)
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
                detail = await _radioProvider.GetRadioDetailInfo(_playItem.MainId, CancellationToken.None);
                count++;
            }
            var snapshot = _playerAdapter.ConvertToPlayItemSnapshot(detail);
            _playItem = snapshot;
            if (detail.UpdateTime != TimeSpan.Zero)
            {
                if (_refreshTimer.Enabled)
                    _refreshTimer.Stop();
                _refreshTimer.Interval = detail.UpdateTime.Add(TimeSpan.FromSeconds(1)).TotalMilliseconds;
                if (!_refreshTimer.Enabled)
                    _refreshTimer.Start();
            }
            else
            {
                if (_refreshTimer.Enabled)
                    _refreshTimer.Stop();
            }
            PlayerItemChanged?.Invoke(this, new PlayerItemChangedEventArgs(
                _playItem));
            UpdateProperties(snapshot);
        }

        private async Task<Uri> GetContentSource(int ContentId, int ProgramId)
        {
            string url = string.Format(ApiConstants.Content.Play, ContentId, ProgramId);
            var query = await _accountProvider.GenerateAuthorizedQueryStringAsync(url, null, RequestType.PlayContent, true);
            url += $"?{query}";
            return new Uri(url);
        }

        private async Task PlayContentDemand(PlayItemSnapshot playItem)
        {
            var uri = await GetContentSource(playItem.MainId, playItem.SecondaryId);
            _currentType = MediaPlayType.ContentDemand;
            _mediaSource = MediaSource.CreateFromUri(uri);
            _mediaPlaybackItem = new MediaPlaybackItem(_mediaSource);
            AudioPlayer.Source = _mediaPlaybackItem;
            _playItem = playItem;
            _canPrevious = _contentPlayListIndex > 0;
            _canNext = _contentPlayListIndex < _playList.Count - 1;
            _transportControls.IsPreviousEnabled = _canPrevious;
            _transportControls.IsNextEnabled = _canNext;
            UpdateProperties(playItem);
            //PlayerItemChanged?.Invoke(this, new PlayerItemChangedEventArgs(
            //    _playItem));
            Play();
        }

        public void Refresh()
        {
            switch (_currentType)
            {
                case MediaPlayType.RadioLive:
                    PlayRadioLive(_playItem.MainId);
                    break;
                case MediaPlayType.RadioDemand:
                    SetPosition(0);
                    break;
                case MediaPlayType.ContentDemand:
                    SetPosition(0);
                    break;
                default:
                    break;
            }
        }
    }
}
