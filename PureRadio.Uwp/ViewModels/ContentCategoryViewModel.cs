using CommunityToolkit.Mvvm.ComponentModel;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Providers;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Media.Animation;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class ContentCategoryViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IContentProvider contentProvider;
        private int page;

        public int AttrId;

        private int _categoryId;
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                SetProperty(ref _categoryId, value);
                if (_categoryId != 0) InitCategoryTitle(_categoryId);
            }
        }

        [ObservableProperty]
        private string _categoryTitle;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _isEmpty;

        public IncrementalLoadingObservableCollection<ContentInfoCategory> ContentResult { get; set; }

        public ContentCategoryViewModel(
            INavigateService navigate,
            IContentProvider contentProvider)
        {
            this.navigate = navigate;
            this.contentProvider = contentProvider;
            page = 1;
            ContentResult = new IncrementalLoadingObservableCollection<ContentInfoCategory>(RequestForContentCategory);
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            page = 1;
            ContentResult.OnStartLoading += StartLoading;
            ContentResult.OnEndLoading += EndLoading;
        }

        protected override void OnDeactivated()
        {
            ContentResult.OnStartLoading -= StartLoading;
            ContentResult.OnEndLoading -= EndLoading;
            base.OnDeactivated();
        }

        private async Task<IEnumerable<ContentInfoCategory>> RequestForContentCategory(CancellationToken cancelToken)
        {
            var resultSet = await contentProvider.GetContentCategoryResult(CategoryId, cancelToken, AttrId, page);
            page++;
            return resultSet.Items;
        }

        private void StartLoading()
        {
            IsLoading = true;
        }

        private void EndLoading()
        {
            IsLoading = false;
            IsEmpty = ContentResult.Count == 0;
        }

        private void InitCategoryTitle(int categoryId)
        {
            if(CategoryTitle == string.Empty)
            {
                var resources = new ResourceLoader();
                CategoryTitle = resources.GetString(AppConstants.RadioCategoryDict[categoryId] ?? "LangRadioCategoryUnknown");
            }          
        }

        public void Navigate(PageIds pageId, object parameter = null)
        {
            if (pageId == PageIds.ContentDetail)
            {
                navigate.NavigateToSecondaryView(pageId, new EntranceNavigationTransitionInfo(), parameter);
            }
        }
    }
}
