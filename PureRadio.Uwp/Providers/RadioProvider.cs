using PureRadio.Uwp.Adapters;
using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.QingTing.Radio;
using PureRadio.Uwp.Models.QingTing.Search;
using PureRadio.Uwp.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            var items = cancellationToken.IsCancellationRequested || result.Data == null
                ? null
                : _radioAdapter.ConvertToRadioInfoDetail(result.Data);
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
    }
}
