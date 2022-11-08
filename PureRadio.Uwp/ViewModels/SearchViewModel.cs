using CommunityToolkit.Mvvm.ComponentModel;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
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

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class SearchViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly ISearchProvider searchProvider;

        [ObservableProperty]
        private string _keyword;
        

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

    }
}
