using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI;
using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Providers;
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
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class MainViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;// = Ioc.Default.GetRequiredService<INavigateService>();
        private readonly ISearchProvider searchProvider;
        private readonly IAccountProvider accountProvider;
        private readonly DispatcherTimer _suggestionTimer;
        private CancellationTokenSource _suggestionCancellationTokenSource;
        private bool _isKeywordChanged;

        public string _noResultTip;

        private string _keyword;
        public string Keyword
        {
            get => _keyword;
            set
            {
                HandleKeywordChanged(value);
                SetProperty(ref _keyword, value);
            }
        }

        [ObservableProperty]
        private AuthorizeState _accountState;

        [ObservableProperty]
        private List<string> _searchSuggest;

        [ObservableProperty]
        private BitmapImage _userPicture;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _userPhone;

        [ObservableProperty]
        private string _userDescription;


        public MainViewModel(
            INavigateService navigate, 
            ISearchProvider searchProvider,
            IAccountProvider accountProvider)
        {
            this.navigate = navigate;
            this.searchProvider = searchProvider;
            this.accountProvider = accountProvider;
            _suggestionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(350),
            };
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            _noResultTip = resourceLoader.GetString("LangSearchNoResultTip");
            UserPicture = new BitmapImage(new Uri("ms-appx:///Assets/Image/DefaultAvatar.png"));
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            AccountState = accountProvider.State;
            GetAccountInfo();
            accountProvider.StateChanged += AccountStateChanged;
            _suggestionTimer.Tick += OnSuggestionTimerTickAsync;
        }

        protected override void OnDeactivated()
        {
            _suggestionTimer.Tick -= OnSuggestionTimerTickAsync;
            accountProvider.StateChanged -= AccountStateChanged;
            base.OnDeactivated();
        }

        public void Navigate(PageIds pageId, object parameter = null)
        {
            var type = pageId.GetHashCode() switch
            {
                < 100 => NavigationType.Main,
                < 1000 => NavigationType.Secondary,
                _ => NavigationType.Player,
            };

            switch (type)
            {
                case NavigationType.Main:
                    navigate.NavigateToMainView(pageId, new EntranceNavigationTransitionInfo(), parameter);
                    break;
                case NavigationType.Secondary:
                    navigate.NavigateToSecondaryView(pageId, new EntranceNavigationTransitionInfo(), parameter);
                    break;
                case NavigationType.Player:
                    navigate.NavigateToPlayView(new EntranceNavigationTransitionInfo(), false);
                    break;
                default:
                    break;
            }
        }

        private async Task LoadSearchSuggestionAsync()
        {
            if (string.IsNullOrEmpty(Keyword))
            {
                return;
            }
            try
            {
                var suggestion = await searchProvider.GetSearchSuggestion(Keyword, _suggestionCancellationTokenSource.Token);
                if (suggestion == null)
                {
                    return;
                }
                SearchSuggest = suggestion.Count > 0 ? suggestion : new List<string>() { _noResultTip };
            }
            catch (TaskCanceledException)
            {
                // 任务中止表示有新的搜索请求或者请求超时，这是预期的错误，不予处理.
            }
            catch (Exception)
            {
                
            }
        }

        private void HandleKeywordChanged(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                // 搜索关键词为空，表示用户或应用清除了内容，此时不进行请求，并重置状态.
                _isKeywordChanged = false;
                _suggestionTimer.Stop();
                InitializeSuggestionCancellationTokenSource();
                SearchSuggest = new List<string>();
            }
            else
            {
                _isKeywordChanged = true;
                if (!_suggestionTimer.IsEnabled)
                {
                    _suggestionTimer.Start();
                }
            }
        }

        private async void OnSuggestionTimerTickAsync(object sender, object e)
        {
            if (_isKeywordChanged)
            {
                _isKeywordChanged = false;
                InitializeSuggestionCancellationTokenSource();
                await LoadSearchSuggestionAsync();
            }
        }

        private void InitializeSuggestionCancellationTokenSource()
        {
            if (_suggestionCancellationTokenSource != null
                && !_suggestionCancellationTokenSource.IsCancellationRequested)
            {
                _suggestionCancellationTokenSource.Cancel();
                _suggestionCancellationTokenSource = null;
            }

            _suggestionCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(1));
        }

        public async Task<bool> TrySignIn(string phone, string password)
        {
            if (AccountState == AuthorizeState.SignedOut)
                return await accountProvider.TrySignInAsync(phone, password);
            else return true;
        }

        public async Task<bool> TrySignOut()
        {
            await accountProvider.SignOutAsync();
            return true;
        }

        private void AccountStateChanged(object sender, AuthorizeStateChangedEventArgs e)
        {
            if (e.OldState == AuthorizeState.SignedOut && e.NewState == AuthorizeState.SignedIn && AccountState == AuthorizeState.SignedOut)
            {
                AccountState = AuthorizeState.SignedIn;
                GetAccountInfo();
            }
            else if (e.OldState == AuthorizeState.SignedIn && e.NewState == AuthorizeState.SignedOut && AccountState == AuthorizeState.SignedIn)
            {
                AccountState = AuthorizeState.SignedOut;
                GetAccountInfo();
            }
        }

        private async void GetAccountInfo()
        {
            UserPicture = await ImageCache.Instance.GetFromCacheAsync(accountProvider.AccountInfo.Avatar);
            UserName = accountProvider.AccountInfo.NickName;
            UserPhone = accountProvider.AccountInfo.PhoneNumber;
            UserDescription = accountProvider.AccountInfo.Signature;
        }
    }
}
