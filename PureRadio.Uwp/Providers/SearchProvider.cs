using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Data.Search;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.QingTing.Search;
using PureRadio.Uwp.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.BulkAccess;

namespace PureRadio.Uwp.Providers
{
    public class SearchProvider : ISearchProvider
    {
        private readonly IHttpProvider _httpProvider;
        private readonly ISearchAdapter _searchAdapter;
        private readonly IRadioAdapter _radioAdapter;
        private readonly IContentAdapter _contentAdapter;

        private int _radioPageNumber;
        private int _contentPageNumber;
        public SearchProvider(
            IHttpProvider httpProvider,
            ISearchAdapter searchAdapter,
            IRadioAdapter radioAdapter,
            IContentAdapter contentAdapter)
        {
            _httpProvider = httpProvider;
            _searchAdapter = searchAdapter;
            _radioAdapter = radioAdapter;
            _contentAdapter = contentAdapter;
            ClearStatus();
        }

        public async Task<List<string>> GetSearchSuggestion(string keyword, CancellationToken cancellationToken)
        {
            Dictionary<string, string> parameters = new()
            {
                {"k", keyword},
            };
            var request = await _httpProvider.GetRequestMessageAsync(ApiConstants.Search.Suggest, HttpMethod.Get, parameters);
            var response = await _httpProvider.SendAsync(request, cancellationToken);
            var result = await _httpProvider.ParseAsync<SearchSuggestResponse>(response);
            return !cancellationToken.IsCancellationRequested
                ? result.Data.Select(p => _searchAdapter.ConvertToSearchSuggest(p)).ToList()
                : null;
        }

        public async Task<SearchSet<RadioInfoSearch>> GetRadioSearchResultAsync(string keyword, CancellationToken cancellationToken)
        {
            Dictionary<string, string> parameters = new()
            {
                {ApiConstants.Search.ParamKeyword, keyword},
                {ApiConstants.Search.ParamPage, _radioPageNumber.ToString() },
                {ApiConstants.Search.ParamType, ApiConstants.Search.TypeRadio },
            };
            var request = await _httpProvider.GetRequestMessageAsync(ApiConstants.Search.Radio, HttpMethod.Post, parameters, RequestType.Search);
            var response = await _httpProvider.SendAsync(request);
            var result = await _httpProvider.ParseAsync<SearchRadioResponse>(response);
            _radioPageNumber++;
            var items = cancellationToken.IsCancellationRequested || result.Data.SearchRadioResults?.SearchDatas == null
                ? new List<RadioInfoSearch>()
                : result.Data.SearchRadioResults.SearchDatas.Select(p => _searchAdapter.ConvertToSearchResultView(p));
            return new SearchSet<RadioInfoSearch>(items, result.Data.SearchRadioResults.ResultCount < _radioPageNumber * 15);
        }


        public async Task<SearchSet<ContentInfoSearch>> GetContentSearchResultAsync(string keyword, CancellationToken cancellationToken)
        {
            Dictionary<string, string> parameters = new()
            {
                {ApiConstants.Search.ParamKeyword, keyword},
                {ApiConstants.Search.ParamPage, _contentPageNumber.ToString() },
                {ApiConstants.Search.ParamType, ApiConstants.Search.TypeContent },
            };
            var request = await _httpProvider.GetRequestMessageAsync(ApiConstants.Search.Radio, HttpMethod.Post, parameters, RequestType.Search);
            var response = await _httpProvider.SendAsync(request);
            var result = await _httpProvider.ParseAsync<SearchContentResponse>(response);
            _contentPageNumber++;
            var items = cancellationToken.IsCancellationRequested || result.Data.SearchContentResults?.SearchDatas == null
                ? new List<ContentInfoSearch>()
                : result.Data.SearchContentResults.SearchDatas.Select(p => _searchAdapter.ConvertToSearchResultView(p));
            return new SearchSet<ContentInfoSearch>(items, result.Data.SearchContentResults.ResultCount < _contentPageNumber * 15);
        }

        public void ResetRadioStatus()
            => _radioPageNumber = 1;

        public void ResetContentStatus()
            => _contentPageNumber = 1;

        public void ClearStatus()
        {
            ResetRadioStatus();
            ResetContentStatus();
        }
    }
}
