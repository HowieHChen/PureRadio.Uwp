using CommunityToolkit.Mvvm.ComponentModel;
using PureRadio.Uwp.Models.Data.Constants;
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
    public sealed partial class RadioCategoryViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IRadioProvider radioProvider;
        private int page;
        private readonly int pageSize;


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

        public IncrementalLoadingObservableCollection<RadioInfoCategory> RadioResult { get; set; }

        public RadioCategoryViewModel(
            INavigateService navigate,
            IRadioProvider radioProvider)
        {
            this.navigate = navigate;
            this.radioProvider = radioProvider;
            page = 1;
            pageSize = 30;
            RadioResult = new IncrementalLoadingObservableCollection<RadioInfoCategory>(RequestForRadioCategory);
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            page = 1;
            RadioResult.OnStartLoading += StartLoading;
            RadioResult.OnEndLoading += EndLoading;
        }

        protected override void OnDeactivated()
        {
            RadioResult.OnStartLoading -= StartLoading;
            RadioResult.OnEndLoading -= EndLoading;
            base.OnDeactivated();
        }

        private async Task<IEnumerable<RadioInfoCategory>> RequestForRadioCategory(CancellationToken cancelToken)
        {
            var resultSet = await radioProvider.GetRadioCategoryResult(CategoryId, cancelToken, page, pageSize);
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
            IsEmpty = RadioResult.Count == 0;
        }

        private void InitCategoryTitle(int categoryId)
        {
            var resources = new ResourceLoader();
            CategoryTitle = resources.GetString(AppConstants.RadioCategoryDict[categoryId] ?? "LangRadioCategoryUnknown");
        }

        public void Navigate(PageIds pageId, object parameter = null)
        {
            if (pageId == PageIds.RadioDetail)
            {
                navigate.NavigateToSecondaryView(pageId, new EntranceNavigationTransitionInfo(), parameter);
            }
        }

    }
}
