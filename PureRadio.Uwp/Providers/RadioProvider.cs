using PureRadio.Uwp.Adapters;
using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Data.Search;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Models.QingTing.Content;
using PureRadio.Uwp.Models.QingTing.Radio;
using PureRadio.Uwp.Models.QingTing.Recommend;
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
using Windows.UI.Xaml.Controls;

namespace PureRadio.Uwp.Providers
{
    public class RadioProvider : IRadioProvider
    {
        private readonly IHttpProvider _httpProvider;
        private readonly IRadioAdapter _radioAdapter;

        public RadioProvider(
            IHttpProvider httpProvider, 
            IRadioAdapter radioAdapter)
        {
            _httpProvider = httpProvider;
            _radioAdapter = radioAdapter;
        }

        public async Task<RadioInfoDetail> GetRadioDetailInfo(int radioId, CancellationToken cancellationToken)
        {
            string url = ApiConstants.Radio.Detail + radioId.ToString();
            var request = await _httpProvider.GetRequestMessageAsync(url, HttpMethod.Get);
            var response = await _httpProvider.SendAsync(request, cancellationToken);
            var result = await _httpProvider.ParseAsync<RadioDetailResponse>(response);
            RadioInfoDetail items;
            if(cancellationToken.IsCancellationRequested || result.Data == null)
            {
                items = null;
            }
            else
            {
                items = _radioAdapter.ConvertToRadioInfoDetail(result.Data);
                if(items.UpdateTime < TimeSpan.Zero)
                {
                    await Task.Delay(2000);
                    request = await _httpProvider.GetRequestMessageAsync(url, HttpMethod.Get);
                    response = await _httpProvider.SendAsync(request, cancellationToken);
                    result = await _httpProvider.ParseAsync<RadioDetailResponse>(response);
                    items = cancellationToken.IsCancellationRequested || result.Data == null
                        ? null
                        : _radioAdapter.ConvertToRadioInfoDetail(result.Data);
                }                
            }
            return items;
        }

        public async Task<RadioPlaylistDetailSet> GetRadioPlaylistDetail(int radioId, CancellationToken cancellationToken)
        {
            string url = ApiConstants.Radio.Detail + radioId.ToString() + "/playbills";
            int[] days =
            {
                (int)DateTime.Today.AddDays(-2).DayOfWeek + 1,
                (int)DateTime.Today.AddDays(-1).DayOfWeek + 1,
                (int)DateTime.Today.DayOfWeek + 1,
                (int)DateTime.Today.AddDays(1).DayOfWeek + 1
            };
            string param = string.Join(',', days);
            Dictionary<string, string> parameters = new()
            {
                {"day", param},
            };
            var request = await _httpProvider.GetRequestMessageAsync(url, HttpMethod.Get, parameters);
            var response = await _httpProvider.SendAsync(request, cancellationToken);
            var result = await _httpProvider.ParseAsync<RadioPlaylistResponse>(response);
            if (cancellationToken.IsCancellationRequested || result.Data == null)
                return null;
            else
            {
                RadioPlaylistDetailSet items = new();
                switch (DateTime.Today.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        items.BYday = result.Data.Friday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Yday = result.Data.Saturday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Today = result.Data.Sunday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Tmr = result.Data.Monday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        break;
                    case DayOfWeek.Monday:
                        items.BYday = result.Data.Saturday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Yday = result.Data.Sunday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Today = result.Data.Monday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Tmr = result.Data.Tuesday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        break;
                    case DayOfWeek.Tuesday:
                        items.BYday = result.Data.Sunday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Yday = result.Data.Monday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Today = result.Data.Tuesday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Tmr = result.Data.Wednesday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        break;
                    case DayOfWeek.Wednesday:
                        items.BYday = result.Data.Monday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Yday = result.Data.Tuesday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Today = result.Data.Wednesday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Tmr = result.Data.Thursday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        break;
                    case DayOfWeek.Thursday:
                        items.BYday = result.Data.Tuesday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Yday = result.Data.Wednesday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Today = result.Data.Thursday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Tmr = result.Data.Friday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        break;
                    case DayOfWeek.Friday:
                        items.BYday = result.Data.Wednesday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Yday = result.Data.Thursday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Today = result.Data.Friday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Tmr = result.Data.Saturday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        break;
                    case DayOfWeek.Saturday:
                        items.BYday = result.Data.Thursday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Yday = result.Data.Friday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Today = result.Data.Saturday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        items.Tmr = result.Data.Sunday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList();
                        break;
                    default:
                        return null;
                }
                return items;
            }
        }

        public async Task<List<RadioPlaylistDetail>> GetRadioPlaylistDetail(int radioId, PlaylistDay day, CancellationToken cancellationToken)
        {
            string url = ApiConstants.Radio.Detail + radioId.ToString() + "/playbills";
            int offset = day switch
            {
                PlaylistDay.BeforeYesterday => -2,
                PlaylistDay.Yesterday => -1,
                PlaylistDay.Tomorrow => 1,
                _ => 0,
            };
            int pDay = (int)DateTime.Today.AddDays(offset).DayOfWeek + 1;
            Dictionary<string, string> parameters = new()
            {
                {"day", pDay.ToString()},
            };
            var request = await _httpProvider.GetRequestMessageAsync(url, HttpMethod.Get, parameters);
            var response = await _httpProvider.SendAsync(request, cancellationToken);
            var result = await _httpProvider.ParseAsync<RadioPlaylistResponse>(response);
            if (cancellationToken.IsCancellationRequested || result.Data == null)
                return null;
            else
            {
                DayOfWeek targetDay = DateTime.Today.AddDays(offset).DayOfWeek;
                List<RadioPlaylistDetail> items = targetDay switch
                {
                    DayOfWeek.Sunday => result.Data.Sunday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList(),
                    DayOfWeek.Monday => result.Data.Monday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList(),
                    DayOfWeek.Tuesday => result.Data.Tuesday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList(),
                    DayOfWeek.Wednesday => result.Data.Wednesday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList(),
                    DayOfWeek.Thursday => result.Data.Thursday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList(),
                    DayOfWeek.Friday => result.Data.Friday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList(),
                    DayOfWeek.Saturday => result.Data.Saturday.Select(p => _radioAdapter.ConvertToRadioPlaylistItem(p)).ToList(),
                    _ => new(),
                };
                return items;
            }
        }


        public async Task<ResultSet<RadioInfoSummary>> GetRadioCategoryResult(int categoryId, CancellationToken cancellationToken, int page = 1, int pageSize = 30)
        {
            Dictionary<string, string> parameters = new()
            {
                {ServiceConstants.Params.CategoryId, categoryId.ToString()},
                {ServiceConstants.Params.Page, page.ToString() },
                {ServiceConstants.Params.PageSize, pageSize.ToString() },
            };
            var request = await _httpProvider.GetRequestMessageAsync(ApiConstants.Radio.Category, HttpMethod.Get, parameters, RequestType.Default);
            var response = await _httpProvider.SendAsync(request);
            var result = await _httpProvider.ParseAsync<RadioCategoryResponse>(response);
            var items = (cancellationToken.IsCancellationRequested || result.Data == null || result.Data?.Count == 0)
                ? new List<RadioInfoSummary>()
                : result.Data.Select(p => _radioAdapter.ConvertToRadioInfoSummary(p)).ToList();
            return new ResultSet<RadioInfoSummary>(items, result.Data?.Count <= 0);
        }

        public async Task<List<RadioInfoRecommend>> GetRadioRecommendResult(CancellationToken cancellationToken)
        {
            var request = await _httpProvider.GetRequestMessageAsync(ApiConstants.Radio.Recommend, HttpMethod.Post, null, RequestType.RecommendRadio);
            var response = await _httpProvider.SendAsync(request);
            var result = await _httpProvider.ParseAsync<RadioRecommendResponse>(response);
            var items = cancellationToken.IsCancellationRequested || result.Data.Radios?.RadioItems == null
                ? new List<RadioInfoRecommend>()
                : result.Data.Radios.RadioItems.Select(p => _radioAdapter.ConvertToRadioInfoRecommend(p)).ToList();
            return items;
        }

        public async Task<List<RadioInfoSummary>> GetRadioBillboardResult(int regionId, CancellationToken cancellationToken)
        {
            string url = string.Format(ApiConstants.Radio.Billboard, regionId);
            var request = await _httpProvider.GetRequestMessageAsync(url, HttpMethod.Get, null, RequestType.Default);
            var response = await _httpProvider.SendAsync(request);
            var result = await _httpProvider.ParseAsync<RadioCategoryResponse>(response);
            var items = (cancellationToken.IsCancellationRequested || result.Data == null || result.Data?.Count == 0)
                ? new List<RadioInfoSummary>()
                : result.Data.Select(p => _radioAdapter.ConvertToRadioInfoSummary(p)).ToList();
            return items;
        }

        public async Task<(List<RadioInfoDetail>, List<RadioReplayInfo>)> GetRadioHomeRecResult(CancellationToken cancellationToken)
        {
            var request = await _httpProvider.GetRequestMessageAsync(ApiConstants.Radio.Recommend, HttpMethod.Post, null, RequestType.HomeRadioRecommend);
            var response = await _httpProvider.SendAsync(request);
            var result = await _httpProvider.ParseAsync<RecommendResponse>(response);
            var live = cancellationToken.IsCancellationRequested || result.Data.RadioData?.LivingRadios == null
                ? new List<RadioInfoDetail>()
                : result.Data.RadioData.LivingRadios.Select(p => _radioAdapter.ConvertToRadioInfoDetail(p)).ToList();
            var replay = cancellationToken.IsCancellationRequested || result.Data.RadioData?.ReplayRadios == null
                ? new List<RadioReplayInfo>()
                : result.Data.RadioData.ReplayRadios.Select(p => _radioAdapter.ConvertToRadioReplayInfo(p)).ToList();
            return (live, replay);
        }
    }
}
