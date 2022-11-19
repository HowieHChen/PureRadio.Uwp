using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI;
using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Models.QingTing.Content;
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
using Windows.UI.Xaml.Media.Imaging;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class ContentDetailViewModel : ObservableRecipient
    {
        private readonly IPlaybackService playbackService;
        private readonly INavigateService navigate;
        private readonly ILibraryService library;
        private readonly IContentProvider contentProvider;
        private readonly IPlayerAdapter playerAdapter;

        private string _version;
        private int _contentId;
        private ContentInfoDetail _contentDetail;
        private PlayItemSnapshot itemSnapshot;
        public int ContentId
        {
            get => _contentId;
            set
            {
                SetProperty(ref _contentId, value);
                GetContentFavState();
                GetContentDetail();
            }
        }
        public IAsyncRelayCommand ToggleFavCommand { get; }

        [ObservableProperty]
        private bool _isFav;
        [ObservableProperty]
        private BitmapImage _cover;
        [ObservableProperty]
        private string _title;
        [ObservableProperty]
        private string _podcasters;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private string _playCount;
        [ObservableProperty]
        private float _rating;
        [ObservableProperty]
        private List<AttributesItem> _attributes;

        [ObservableProperty]
        private List<ContentPlaylistDetail> _contentPlaylists;

        [ObservableProperty]
        private bool _isInfoLoading;

        [ObservableProperty]
        private bool _isPlaylistLoading;

        public ContentDetailViewModel(
            IPlaybackService playbackService,
            INavigateService navigate,
            ILibraryService library, 
            IContentProvider contentProvider,
            IPlayerAdapter playerAdapter)
        {
            this.playbackService = playbackService;
            this.navigate = navigate;
            this.library = library;
            this.contentProvider = contentProvider;
            this.playerAdapter = playerAdapter;
            Cover = new BitmapImage(new Uri("ms-appx:///Assets/Image/DefaultCover.png"));
            ToggleFavCommand = new AsyncRelayCommand(ToggleFavState);
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            library.FavItemChanging += Library_FavItemChanging;
        }

        protected override void OnDeactivated()
        {
            library.FavItemChanging -= Library_FavItemChanging;
            base.OnDeactivated();
        }

        private void Library_FavItemChanging(object sender, FavItemChangedEventArgs e)
        {
            if (e.ItemType == MediaPlayType.ContentDemand && e.MainId == ContentId)
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

        private async void GetContentFavState()
        {
            if (ContentId != 0)
            {
                IsFav = await library.IsFavItem(MediaPlayType.ContentDemand, ContentId);
            }
        }

        private async void GetContentDetail()
        {
            if(ContentId != 0)
            {
                IsInfoLoading = true;
                var result = await contentProvider.GetContentDetailInfo(ContentId, CancellationToken.None);
                if (result != null) 
                {
                    Cover = await ImageCache.Instance.GetFromCacheAsync(result.Cover);
                    Cover.DecodePixelHeight = Cover.DecodePixelWidth = 200;
                    Cover.DecodePixelType = DecodePixelType.Logical;
                    Title = result.Title;
                    Podcasters = result.Podcasters;
                    PlayCount = result.PlayCount;
                    Rating = result.Rating;
                    Description = result.Description;
                    Attributes = result.Attributes;
                    _version = result.Version;
                    _contentDetail = result;
                    itemSnapshot = playerAdapter.ConvertToPlayItemSnapshot(result);
                }
                IsInfoLoading = false;
                if (!string.IsNullOrEmpty(_version))
                {
                    GetContentPlaylist();
                }
            }
        }

        private async void GetContentPlaylist()
        {
            if (ContentId != 0 && !string.IsNullOrEmpty(_version))
            {
                IsPlaylistLoading = true;
                var result = await contentProvider.GetContentProgramListFull(ContentId, _version, CancellationToken.None);
                if (result != null)
                {
                    ContentPlaylists = result;
                }
                IsPlaylistLoading = false;
            }
        }

        public void PlayContent(int programId = 0)
        {
            if (ContentId != 0)
            {
                if (programId == 0) programId = ContentPlaylists[0].ProgramId;
                //var playlist = playerAdapter.ConvertToPlayItemSnapshotList(_contentDetail, ContentPlaylists);
                //playbackService.PlayContent(ContentId, programId, playlist);
                playbackService.PlayContent(ContentId, programId, _version);
            }
        }

        public async Task ToggleFavState()
        {
            if (ContentId != 0 && itemSnapshot != null)
            {
                if (IsFav)
                {
                    _ = await library.RemoveFromFav(MediaPlayType.ContentDemand, ContentId);
                }
                else
                {
                    _ = await library.AddToFav(itemSnapshot);
                }
            }
        }

        public void NavigateToCategory(AttributesItem attributes)
        {
            if (attributes.CategoryId != 0 && attributes.AttrId != 0)
            {
                navigate.NavigateToSecondaryView(PageIds.ContentCategory, new EntranceNavigationTransitionInfo(), new ContentCategoryEventArgs(
                    attributes.CategoryId, attributes.AttrId, attributes.Name));
            }
        }
    }
}
