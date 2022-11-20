using CommunityToolkit.Mvvm.ComponentModel;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class ContentViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IContentProvider contentProvider;

        [ObservableProperty]
        private bool _isItemsLoading;
        [ObservableProperty]
        private bool _isEmpty;
        public ObservableCollection<ContentRecommendSet> Categories;

        public ContentViewModel(
            INavigateService navigate,
            IContentProvider contentProvider)
        {
            this.navigate = navigate;
            this.contentProvider = contentProvider;
            Categories = new ObservableCollection<ContentRecommendSet>();

            GetRecommendContents();
        }

        public async void GetRecommendContents()
        {
            IsItemsLoading = true;
            Categories.Clear();
            var categories = await contentProvider.GetContentRecommendResult(CancellationToken.None);
            foreach(var category in categories)
            {
                Categories.Add(category);
            }
            IsEmpty = Categories.Count == 0;
            IsItemsLoading = false;
        }

        public void Navigate(PageIds pageId, object parameter = null)
        {
            if (pageId == PageIds.ContentCategory || pageId == PageIds.ContentDetail)
            {
                navigate.NavigateToSecondaryView(pageId, new EntranceNavigationTransitionInfo(), parameter);
            }
        }
    }
}
