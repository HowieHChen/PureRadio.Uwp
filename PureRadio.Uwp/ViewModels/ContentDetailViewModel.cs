using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp.UI;
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
        private string _title;
        [ObservableProperty]
        private BitmapImage _cover;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private string _playCount;
        [ObservableProperty]
        private float _rating;
        [ObservableProperty]
        private string _topCategoryTitle;

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
                    Title = result.Title;
                    Cover = await ImageCache.Instance.GetFromCacheAsync(result.Cover);
                    Description = result.Description;
                    PlayCount = result.PlayCount;
                    Rating = result.Rating;
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

                }
                IsPlaylistLoading = false;
            }
        }
    }
}
