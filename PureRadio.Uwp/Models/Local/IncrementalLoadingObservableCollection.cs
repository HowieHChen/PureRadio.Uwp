using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace PureRadio.Uwp.Models.Local
{
    public class IncrementalLoadingObservableCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private readonly Func<CancellationToken, Task<IEnumerable<T>>> _provideMoreItems;
        public Action OnStartLoading { get; set; }
        public Action OnEndLoading { get; set; }
        public Action<Exception> OnError { get; set; }

        private bool _isLoading;
        private bool _hasMoreItems;
        private CancellationToken _cancellationToken;

        public IncrementalLoadingObservableCollection(Func<CancellationToken, Task<IEnumerable<T>>> provideMoreItems)
        {
            _provideMoreItems = provideMoreItems;
            _hasMoreItems = true;
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            private set
            {
                if (value != _isLoading)
                {
                    _isLoading = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsLoading)));

                    if (_isLoading)
                    {
                        OnStartLoading?.Invoke();
                    }
                    else
                    {
                        OnEndLoading?.Invoke();
                    }
                }
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(async cancelToken =>
            {
                uint resultCount = 0;
                _cancellationToken = cancelToken;
                try
                {
                    if (!_cancellationToken.IsCancellationRequested)
                    {
                        IEnumerable<T> data = null;
                        try
                        {
                            IsLoading = true;
                            data = await _provideMoreItems(_cancellationToken);
                        }
                        catch (OperationCanceledException)
                        {
                            // The operation has been canceled using the Cancellation Token.
                        }
                        catch (Exception ex) when (OnError != null)
                        {
                            OnError.Invoke(ex);
                        }

                        if (data != null && data.Any() && !_cancellationToken.IsCancellationRequested)
                        {
                            resultCount = (uint)data.Count();
                            foreach (var item in data)
                            {
                                Add(item);
                            }
                        }
                        else
                        {
                            HasMoreItems = false;
                        }
                    }
                }
                finally
                {
                    IsLoading = false;
                }
                return new LoadMoreItemsResult { Count = resultCount };
            });
        }

        public bool HasMoreItems
        {
            get
            {
                if (_cancellationToken.IsCancellationRequested)
                {
                    return false;
                }

                return _hasMoreItems;
            }

            private set
            {
                if (value != _hasMoreItems)
                {
                    _hasMoreItems = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(HasMoreItems)));
                }
            }
        }
    }
}
