using PureRadio.Uwp.Adapters;
using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Data.Search;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Models.QingTing.Content;
using PureRadio.Uwp.Models.QingTing.Radio;
using PureRadio.Uwp.Models.QingTing.Search;
using PureRadio.Uwp.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Providers
{
    public class ContentProvider : IContentProvider
    {
        private readonly IHttpProvider _httpProvider;
        private readonly IContentAdapter _contentAdapter;

        public ContentProvider(
            IHttpProvider httpProvider, 
            IContentAdapter contentAdapter)
        {
            _httpProvider = httpProvider;
            _contentAdapter = contentAdapter;
        }

        public async Task<ContentInfoDetail> GetContentDetailInfo(int contentId, CancellationToken cancellationToken)
        {
            string url = ApiConstants.Content.Detail + contentId.ToString();
            var request = await _httpProvider.GetRequestMessageAsync(url, HttpMethod.Get);
            var response = await _httpProvider.SendAsync(request, cancellationToken);
            var result = await _httpProvider.ParseAsync<ContentDetailResponse>(response);
            var items = cancellationToken.IsCancellationRequested || result.Data == null
                ? null
                : _contentAdapter.ConvertToContentInfoDetail(result.Data, result.Attributes);
            return items;
        }

        public async Task<List<ContentPlaylistDetail>> GetContentProgramListFull(int contentId, string version, bool isContentFree, CancellationToken cancellationToken)
        {
            List<ContentPlaylistDetail> items = new();
            string url = ApiConstants.Content.PList + contentId.ToString() + "/programs/" + version.ToString();
            int _contentPageNumber = 1;
            int _total = 0;
            do
            {
                Dictionary<string, string> parameters = new()
                {
                    {ServiceConstants.Params.CurPage, _contentPageNumber.ToString()},
                    {ServiceConstants.Params.PageSize, 30.ToString() },
                    {ServiceConstants.Params.Order, ServiceConstants.Order.Asc },
                };
                var request = await _httpProvider.GetRequestMessageAsync(url, HttpMethod.Get, parameters);
                var response = await _httpProvider.SendAsync(request, cancellationToken);
                var result = await _httpProvider.ParseAsync<ContentProgramListResponse>(response);
                _contentPageNumber++;
                if (cancellationToken.IsCancellationRequested || result.Data == null)
                {
                    break;
                }
                else
                {
                    _total = result.Data.Total;
                    items.AddRange(result.Data.Programs.Select(p => _contentAdapter.ConvertToContentPlaylistItem(p, version, isContentFree)).ToList());
                }
            }
            while (items.Count < _total);
            return items;
        }

        public async Task<ResultSet<ContentInfoCategory>> GetContentCategoryResult(int categoryId, CancellationToken cancellationToken, int attrId = 0, int page = 1)
        {
            Dictionary<string, string> parameters = new()
            {
                {ServiceConstants.Params.Category, categoryId.ToString()},
                {ServiceConstants.Params.Attributes, attrId.ToString() },
                {ServiceConstants.Params.CurPage, page.ToString() },
            };
            var request = await _httpProvider.GetRequestMessageAsync(ApiConstants.Content.Category, HttpMethod.Get, parameters, RequestType.Default);
            var response = await _httpProvider.SendAsync(request);
            var result = await _httpProvider.ParseAsync<ContentCategoryResponse>(response);
            var items = (cancellationToken.IsCancellationRequested || result.Data.Contents == null || result.Data.Contents?.Count == 0)
                ? new List<ContentInfoCategory>()
                : result.Data.Contents.Select(p => _contentAdapter.ConvertToContentInfoCategory(p)).ToList();
            return new ResultSet<ContentInfoCategory>(items, result.Total <= page * 12);
        }

        public async Task<List<ContentRecommendSet>> GetContentRecommendResult(CancellationToken cancellationToken)
        {
            var request = await _httpProvider.GetRequestMessageAsync(ApiConstants.Content.Recommend, HttpMethod.Post, null, RequestType.RecommendContent);
            var response = await _httpProvider.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            responseString = Regex.Replace(responseString, "\\{\"type\":\"Banner\"(.)*]\\}\\},", string.Empty);
            var result = _httpProvider.ParseAsync<ContentRecommendResponse>(responseString);
            var items = cancellationToken.IsCancellationRequested || result.Data.Contents?.Categories == null
                ? new List<ContentRecommendSet>()
                : result.Data.Contents.Categories.Select(p => _contentAdapter.ConvertToContentRecommendSet(p)).ToList();
            return items;
        }
    }
}
