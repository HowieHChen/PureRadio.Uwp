using CommunityToolkit.Mvvm.DependencyInjection;
using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Data.User;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.QingTing.User;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Documents;
using static PureRadio.Uwp.Models.Data.Constants.ServiceConstants;

namespace PureRadio.Uwp.Providers
{
    public class AccountProvider : IAccountProvider
    {
        private readonly ISettingsService settings;// = Ioc.Default.GetRequiredService<ISettingsService>();
        private readonly IAccountAdapter accountAdapter;
        private AuthorizeState _state;
        private AccountInfo _accountInfo;
        private TokenInfo _tokenInfo;
        private DateTimeOffset _lastAuthorizeTime;

        public AccountProvider(ISettingsService settings, IAccountAdapter accountAdapter)
        {
            this.settings = settings;
            this.accountAdapter = accountAdapter;
        }

        public event EventHandler<AuthorizeStateChangedEventArgs> StateChanged;

        public AuthorizeState State
        {
            get => _state;
            protected set
            {
                var oldState = _state;
                var newState = value;
                if (oldState != newState)
                {
                    _state = newState;
                    StateChanged?.Invoke(this, new AuthorizeStateChangedEventArgs(oldState, newState));
                }
            }
        }

        public AccountInfo AccountInfo
        {
            get => _accountInfo;
            protected set
            {
                _accountInfo = value;
            }
        }

        public async Task<string> GenerateAuthorizedQueryStringAsync(
            string url, 
            Dictionary<string, string> parameters, 
            RequestType requestType = RequestType.Default, 
            bool needToken = false)
        {
            var param = await GenerateAuthorizedQueryDictionaryAsync(url, parameters, requestType, needToken);
            var queryList = param.Select(p => $"{p.Key}={p.Value}").ToList();
            queryList.Sort();
            var query = string.Join('&', queryList);
            return query;
        }

        public async Task<Dictionary<string, string>> GenerateAuthorizedQueryDictionaryAsync(
            string url, 
            Dictionary<string, string> parameters, 
            RequestType requestType = RequestType.Default, 
            bool needToken = false)
        {
            parameters ??= new Dictionary<string, string>();

            var token = string.Empty;
            if (IsTokenValidAsync())
            {
                token = _tokenInfo.AccessToken;
            }
            else if (needToken)
            {
                token = await GetTokenAsync();
            }

            var qingtingId = _tokenInfo is null ? string.Empty : _tokenInfo?.QingtingId;

            switch (requestType)
            {
                default:
                case RequestType.Default:
                    if (needToken)
                    {
                        if (!string.IsNullOrEmpty(token)) parameters.Add(ServiceConstants.Params.AccessToken, token);
                        else throw new OperationCanceledException("需要令牌，但获取访问令牌失败.");
                    }
                    parameters.Add(ServiceConstants.Params.QingTingId, qingtingId);
                    break;
                case RequestType.Login:
                    parameters.Add(ServiceConstants.Query.AccountType, "5");
                    parameters.Add(ServiceConstants.Query.DeviceId, "web");
                    parameters.Add(ServiceConstants.Query.AreaCode, "+86");
                    break;
                case RequestType.Auth:
                    var refreshToken = _tokenInfo.RefreshToken;
                    parameters.Add(ServiceConstants.Query.GrantType, ServiceConstants.Query.RefreshToken);
                    parameters.Add(ServiceConstants.Query.RefreshToken, refreshToken);
                    parameters.Add(ServiceConstants.Query.QingTingId, qingtingId);
                    break;
                case RequestType.Search:

                    break;
                case RequestType.PlayContent:
                    parameters.Add(ServiceConstants.Params.AccessToken, token);
                    parameters.Add(ServiceConstants.Params.DeviceId, "MOBILESITE");
                    parameters.Add(ServiceConstants.Params.QingTingId, qingtingId);
                    parameters.Add(ServiceConstants.Params.Time, DateTimeOffset.Now.ToUnixTimeSeconds().ToString());
                    var sign = GenerateSign(url, parameters);
                    parameters.Add(ServiceConstants.Params.Sign, sign);
                    break;
            }
            return parameters;
        }

        public async Task<string> GetTokenAsync()
        {
            try
            {
                if (_tokenInfo != null)
                {
                    if (IsTokenValidAsync())
                    {
                        State = AuthorizeState.SignedIn;
                        return _tokenInfo.AccessToken;
                    }
                    else
                    {
                        var tokenInfo = await InternalRefreshTokenAsync();
                        if (tokenInfo != null)
                        {
                            return tokenInfo;
                        }
                    }
                }
            }
            catch (Exception)
            {
                await SignOutAsync();
                throw;
            }

            return default;
        }

        /// <inheritdoc/>
        public async Task<bool> TrySignInAsync()
        {
            if (IsTokenValidAsync() || State == AuthorizeState.SignedIn)
            {
                return true;
            }
            try
            {
                if (_tokenInfo == null && settings.GetValue<bool>(AppConstants.SettingsKey.AccountOnline))
                {
                    var phoneNumber = settings.GetValue<string>(AppConstants.SettingsKey.AccountPhone);
                    var password = settings.GetValue<string>(AppConstants.SettingsKey.AccountPassword);
                    var parameters = new Dictionary<string, string>
                    {
                        { Query.UserId, phoneNumber },
                        { Query.Password, password },
                    };
                    var httpProvider = Ioc.Default.GetRequiredService<IHttpProvider>();
                    var request = await httpProvider.GetRequestMessageAsync(ApiConstants.Account.Login, HttpMethod.Post, parameters, RequestType.Login, false, false);
                    var response = await httpProvider.SendAsync(request);
                    var result = await httpProvider.ParseAsync<SignInResponse>(response);
                    if (result.ErrorNo == 0)
                    {
                        var solved = accountAdapter.ConvertToAccountInfo(result.Data, phoneNumber);
                        AccountInfo = solved.Item1;
                        _tokenInfo = solved.Item2;
                        State = AuthorizeState.SignedIn;
                        _lastAuthorizeTime = DateTimeOffset.Now;
                        return true;
                    }
                }
            }
            catch
            {

            }
            await SignOutAsync();
            return false;
        }

        /// <inheritdoc/>
        public Task SignOutAsync()
        {
            settings.SetValue(AppConstants.SettingsKey.AccountOnline, false);
            settings.SetValue(AppConstants.SettingsKey.AccountPhone, string.Empty);
            settings.SetValue(AppConstants.SettingsKey.AccountPassword, string.Empty);
            if (_tokenInfo != null)
            {
                _tokenInfo = null;
            }
            if (AccountInfo == null || AccountInfo.IsOnline)
            {
                AccountInfo = new();
            }
            State = AuthorizeState.SignedOut;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public bool IsTokenValidAsync()
        {
            var isLocalValid = _tokenInfo != null &&
                !string.IsNullOrEmpty(_tokenInfo.AccessToken) &&
                _lastAuthorizeTime != null &&
                (DateTimeOffset.Now - _lastAuthorizeTime).TotalSeconds < _tokenInfo.ExpiresIn;
            return isLocalValid;
        }

        internal async Task<string> InternalRefreshTokenAsync()
        {
            var httpProvider = Ioc.Default.GetRequiredService<IHttpProvider>();
            var request = await httpProvider.GetRequestMessageAsync(ApiConstants.Account.RefreshToken, HttpMethod.Post, null, RequestType.Auth, false, false);
            var response = await httpProvider.SendAsync(request);
            var result = await httpProvider.ParseAsync<TokenInfoResponse>(response);
            if (result.ErrorNo != 0)
            {
                _tokenInfo = result.Data;
                State = AuthorizeState.SignedIn;
                _lastAuthorizeTime = DateTimeOffset.Now;
                return _tokenInfo.AccessToken;
            }
            await SignOutAsync();
            return null;
        }

        internal string GenerateSign(
            string url, 
            Dictionary<string, string> parameters,
            RequestType requestType = RequestType.PlayContent)
        {
            var sign = string.Empty;
            if (requestType == RequestType.PlayContent)
            {
                url = url.Replace("https://audio.qtfm.cn", string.Empty);
                var queryList = parameters.Select(p => $"{p.Key}={p.Value}").ToList();
                var query = string.Join('&', queryList);
                var signQuery = url + $"?{query}";
                sign = HmacMD5(signQuery, ApiConstants.Key.Content);
            }
            return sign;
        }

        internal string HmacMD5(string source, string key)
        {
            HMACMD5 hMACMD5 = new HMACMD5(Encoding.Default.GetBytes(key));
            byte[] bytes = hMACMD5.ComputeHash(Encoding.Default.GetBytes(source));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("X2").ToLower());
            }
            hMACMD5.Clear();
            return stringBuilder.ToString();
        }

        public async Task<bool> TrySignInAsync(string phone, string password)
        {
            settings.SetValue<bool>(AppConstants.SettingsKey.AccountOnline, true);
            settings.SetValue<string>(AppConstants.SettingsKey.AccountPhone, phone);
            settings.SetValue<string>(AppConstants.SettingsKey.AccountPassword, password);
            return await TrySignInAsync();
        }
    }
}
