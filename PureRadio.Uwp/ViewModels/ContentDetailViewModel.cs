using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.UI;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.QingTing.Content;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class ContentDetailViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IContentProvider contentProvider;

        private string _version;
        private int _contentId;
        public int ContentId
        {
            get => _contentId;
            set
            {
                SetProperty(ref _contentId, value);
                GetContentDetail();
            }
        }

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
            INavigateService navigate, 
            IContentProvider contentProvider)
        {
            this.navigate = navigate;
            this.contentProvider = contentProvider;
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {

            base.OnDeactivated();
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
                    Title = result.Title;
                    Podcasters = result.Podcasters;
                    PlayCount = result.PlayCount;
                    Rating = result.Rating;
                    Description = result.Description;
                    Attributes = result.Attributes;
                    _version = result.Version;
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
    }
}
