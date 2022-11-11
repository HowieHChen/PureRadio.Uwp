using Newtonsoft.Json;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static PureRadio.Uwp.Models.Data.Constants.ApiConstants;

namespace PureRadio.Uwp.Providers
{
    public class HttpProvider : IHttpProvider, IDisposable
    {
        private readonly IAccountProvider _accountProvider;
        private HttpClient _httpClient;
        private bool _disposedValue;
        private CookieContainer _cookieContainer;

        public HttpProvider(IAccountProvider accountProvider)
        {
            _accountProvider = accountProvider;
            InitHttpClient();
        }

        private void InitHttpClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            _cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.None,
                UseCookies = true,
                CookieContainer = _cookieContainer,
            };
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = false, NoStore = false };
            client.DefaultRequestHeaders.Add("accept", ServiceConstants.DefaultAcceptString);
            _httpClient = client;
        }

        //public async Task<HttpRequestMessage> GetRequestMessageAsync(
        //    string url,
        //    HttpMethod method,
        //    Dictionary<string, string> parameters = null,
        //    RequestType requestType = RequestType.Default,
        //    bool needToken = false,
        //    bool needSign = false)
        //{
        //    HttpRequestMessage requestMessage;
        //    if (method == HttpMethod.Get)
        //    {
        //        var query = await _accountProvider.GenerateAuthorizedQueryStringAsync(url, parameters, requestType, needToken);
        //        url += $"?{query}";
        //        requestMessage = new HttpRequestMessage(method, url);
        //    }
        //    else
        //    {

        //        var query = await _accountProvider.GenerateAuthorizedQueryDictionaryAsync(url, parameters, requestType, needToken);
        //        requestMessage = new HttpRequestMessage(method, url);
        //        requestMessage.Content = new FormUrlEncodedContent(query);
        //    }
        //    return requestMessage;
        //}

        public async Task<HttpRequestMessage> GetRequestMessageAsync(
            string url, 
            HttpMethod method,
            Dictionary<string, string> parameters = null,
            RequestType requestType = RequestType.Default,
            bool needToken = false,
            bool needSign = false)
        {
            HttpRequestMessage requestMessage;
            if (method == HttpMethod.Get)
            {
                var query = await _accountProvider.GenerateAuthorizedQueryStringAsync(url, parameters, requestType, needToken);
                url += $"?{query}";
                requestMessage = new HttpRequestMessage(method, url);
            }
            else
            {
                var query = await _accountProvider.GenerateAuthorizedQueryDictionaryAsync(url, parameters, requestType, needToken);
                requestMessage = new HttpRequestMessage(method, url);
                var content = string.Empty;
                switch (requestType)
                {
                    default:
                    case RequestType.Default:

                        break;
                    case RequestType.Login:
                        var queryList = query.Select(p => $"\"{p.Key}\":\"{p.Value}\"").ToList();
                        queryList.Sort();
                        content = "{" + string.Join(',', queryList) + "}";
                        break;
                    case RequestType.Auth:
                        queryList = query.Select(p => $"\"{p.Key}\":\"{p.Value}\"").ToList();
                        queryList.Sort();
                        content = "{" + string.Join(',', queryList) + "}";
                        break;
                    case RequestType.Search:
                        content = string.Format(ApiConstants.Search.SearchFormat,
                            parameters[ApiConstants.Search.ParamKeyword] ?? string.Empty,
                            parameters[ApiConstants.Search.ParamPage] ?? "1",
                            parameters[ApiConstants.Search.ParamType] ?? ApiConstants.Search.TypeRadio);
                        break;
                }
                requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            return requestMessage;
        }

        internal async Task<HttpResponseMessage> SendRequestAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            try
            {
                await Task.Run(async () =>
                {
                    response = await _httpClient.SendAsync(request, cancellationToken);
                });
            }
            catch (TaskCanceledException exception)
            {
                throw exception;
            }
            catch (HttpRequestException exception)
            {
                throw exception;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public TimeSpan OverallTimeout
        {
            get => _httpClient.Timeout;
            set
            {
                try
                {
                    _httpClient.Timeout = value;
                }
                catch (InvalidOperationException exception)
                {
                    throw exception;
                }
            }
        }

        public HttpClient HttpClient { get => _httpClient; }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
            => SendAsync(request, CancellationToken.None);
        

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await SendRequestAsync(request, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        /// <inheritdoc/>
        public async Task<T> ParseAsync<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose object.
        /// </summary>
        /// <param name="disposing">Is it disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (this._httpClient != null)
                    {
                        this._httpClient.Dispose();
                    }
                }

                this._httpClient = null;
                _disposedValue = true;
            }
        }

    }
}
