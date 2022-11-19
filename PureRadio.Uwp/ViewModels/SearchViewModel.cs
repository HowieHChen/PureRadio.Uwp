using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI.Controls;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
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
using System.Windows.Input;
using Windows.UI.Xaml.Media.Animation;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class SearchViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly ISearchProvider searchProvider;
        private bool _isRadioLoaded;
        private bool _isContentLoaded;

        [ObservableProperty]
        private string _keyword;

        [ObservableProperty]
        private bool _isRadioModuleShown;

        [ObservableProperty]
        private bool _isContentModuleShown;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _isEmpty;

        public ICommand RadioResultCommand { get; }

        public ICommand ContentResultCommand { get; }

        public IncrementalLoadingObservableCollection<RadioInfoSearch> RadioResult { get; set; }
        public IncrementalLoadingObservableCollection<ContentInfoSearch> ContentResult { get; set; }

        public SearchViewModel(
            INavigateService navigate, 
            ISearchProvider searchProvider)
        {
            this.navigate = navigate;
            this.searchProvider = searchProvider;

            searchProvider.ClearStatus();
            RadioResult = new IncrementalLoadingObservableCollection<RadioInfoSearch>(SearchForRadio);
            ContentResult = new IncrementalLoadingObservableCollection<ContentInfoSearch>(SearchForContent);

            RadioResultCommand = new RelayCommand(SetRadioResult);
            ContentResultCommand = new RelayCommand(SetContentResult);

            IsActive = true;

            IsRadioModuleShown = true;
            IsContentModuleShown = false;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            searchProvider.ClearStatus();
            _isRadioLoaded = _isContentLoaded = false;
            RadioResult.OnStartLoading += StartLoading;
            RadioResult.OnEndLoading += EndLoading;
            ContentResult.OnStartLoading += StartLoading;
            ContentResult.OnEndLoading += EndLoading;
        }

        protected override void OnDeactivated()
        {
            RadioResult.OnStartLoading -= StartLoading;
            RadioResult.OnEndLoading -= EndLoading;
            ContentResult.OnStartLoading -= StartLoading;
            ContentResult.OnEndLoading -= EndLoading;
            base.OnDeactivated();
        }

        private async Task<IEnumerable<RadioInfoSearch>> SearchForRadio(CancellationToken cancelToken)
        {
            var resultSet = await searchProvider.GetRadioSearchResultAsync(Keyword, cancelToken);
            return resultSet.Items;
        }

        private async Task<IEnumerable<ContentInfoSearch>> SearchForContent(CancellationToken cancelToken)
        {
            var resultSet = await searchProvider.GetContentSearchResultAsync(Keyword, cancelToken);
            return resultSet.Items;
        }

        private void StartLoading()
        {
            IsLoading = true;
        }

        private void EndLoading()
        {
            IsLoading = false;
            if (IsRadioModuleShown)
            {
                IsEmpty = RadioResult.Count == 0;
                _isRadioLoaded = true;
            }
            else if (IsContentModuleShown)
            {
                IsEmpty = ContentResult.Count == 0;
                _isContentLoaded = true;
            }
            else
            {
                IsEmpty = true;
            }
        }

        private void SetRadioResult()
        {
            if (IsContentModuleShown)
            {
                IsContentModuleShown = false;
            }
            IsRadioModuleShown = true;
            if (_isRadioLoaded) IsEmpty = RadioResult.Count == 0;
        }

        private void SetContentResult()
        {
            if (IsRadioModuleShown)
            {
                IsRadioModuleShown= false;
            }
            IsContentModuleShown = true;
            if (_isContentLoaded) IsEmpty = ContentResult.Count == 0;
        }

        public void Navigate(PageIds pageId, object parameter = null)
        {
            if (pageId == PageIds.RadioDetail || pageId == PageIds.ContentDetail)
            {
                navigate.NavigateToSecondaryView(pageId, new EntranceNavigationTransitionInfo(), parameter);
            }
        }
    }
}
