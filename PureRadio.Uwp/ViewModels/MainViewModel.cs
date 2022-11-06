using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
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
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class MainViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;// = Ioc.Default.GetRequiredService<INavigateService>();
        private readonly ISearchProvider searchProvider;
        private readonly DispatcherTimer _suggestionTimer;
        private CancellationTokenSource _suggestionCancellationTokenSource;
        private bool _isKeywordChanged;

        private string _keyword;
        public string Keyword
        {
            get => _keyword;
            set
            {
                HandleKeywordChanged(value);
                _keyword = value;
            }
        }

        [ObservableProperty]
        private List<string> searchSuggest;

        public MainViewModel(
            INavigateService navigate, 
            ISearchProvider searchProvider)
        {
            this.navigate = navigate;
            this.searchProvider = searchProvider;
            _suggestionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(350),
            };
            _suggestionTimer.Tick += OnSuggestionTimerTickAsync;
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
                    navigate.NavigateToMainView(pageId, parameter);
                    break;
                case NavigationType.Secondary:
                    navigate.NavigateToSecondaryView(pageId, parameter);
                    break;
                case NavigationType.Player:
                    navigate.NavigateToPlayView((PlaySnapshot)parameter);
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
                SearchSuggest = suggestion;
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
    }
}
