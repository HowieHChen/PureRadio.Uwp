using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PureRadio.Uwp.Models.Database;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Animation;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class LibraryViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly ILibraryService library;

        [ObservableProperty]
        private bool _isFavRadioShown;
        [ObservableProperty]
        private bool _isFavContentShown;
        [ObservableProperty]
        private bool _isHistoryShown;

        [ObservableProperty]
        private bool _isLoading;
        [ObservableProperty]
        private bool _isItemsLoading;
        [ObservableProperty]
        private bool _isEmpty;

        public ICommand FavRadioCommand { get; }
        public ICommand FavContentCommand { get; }
        public ICommand HistoryCommand { get; }
        public IAsyncRelayCommand ClearHistoryCommand { get; }

        public ObservableCollection<FavRadio> FavRadioResults;
        public ObservableCollection<FavContent> FavContentResults;
        public ObservableCollection<History> HistoryResults;

        public LibraryViewModel(
            INavigateService navigate, 
            ILibraryService library)
        {
            this.navigate = navigate;
            this.library = library;

            FavRadioResults = new ObservableCollection<FavRadio>();
            FavContentResults = new ObservableCollection<FavContent>();
            HistoryResults = new ObservableCollection<History>();

            FavRadioCommand = new RelayCommand(SetRadioResult);
            FavContentCommand = new RelayCommand(SetContentResult);
            HistoryCommand = new RelayCommand(SetHistoryResult);
            ClearHistoryCommand = new AsyncRelayCommand(ClearHistory);

            GetFavAndHistory();

            IsActive = true;

            IsFavRadioShown = true;
            IsFavContentShown = IsHistoryShown = false;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            library.FavItemChanging += Library_FavItemChanging;
            library.HistoryItemAdded += Library_HistoryItemAddedAsync;
        }

        protected override void OnDeactivated()
        {
            library.FavItemChanging -= Library_FavItemChanging;
            library.HistoryItemAdded -= Library_HistoryItemAddedAsync;
            base.OnDeactivated();
        }

        private void SetRadioResult()
        {
            if (IsFavContentShown || IsHistoryShown)
            {
                IsFavContentShown = IsHistoryShown = false;
            }
            IsFavRadioShown = true;
            IsEmpty = FavRadioResults.Count == 0;
        }

        private void SetContentResult()
        {
            if (IsFavRadioShown || IsHistoryShown)
            {
                IsFavRadioShown = IsHistoryShown = false;
            }
            IsFavContentShown = true;
            IsEmpty = FavContentResults.Count == 0;
        }

        private void SetHistoryResult()
        {
            if (IsFavRadioShown || IsFavContentShown)
            {
                IsFavRadioShown = IsFavContentShown = false;
            }
            IsHistoryShown = true;
            IsEmpty = HistoryResults.Count == 0;
        }

        private async Task ClearHistory()
        {
            IsLoading = true;
            var result = await library.ClearHistory();
            if (result)
            {
                HistoryResults.Clear();
                if (IsHistoryShown) IsEmpty = true;
            }                
            IsLoading = false;
        }

        public async void RemoveFavRadio(FavRadio radio)
        {
            await library.RemoveFromFav(radio);
        }

        public async void RemoveFavContent(FavContent content)
        {
            await library.RemoveFromFav(content);
        }

        public void Navigate(PageIds pageId, object parameter = null)
        {
            if (pageId == PageIds.RadioDetail || pageId == PageIds.ContentDetail)
            {
                navigate.NavigateToSecondaryView(pageId, new EntranceNavigationTransitionInfo(), parameter);
            }
        }

        private async void Library_HistoryItemAddedAsync(object sender, Models.Args.HistoryItemChangedEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                IsLoading = true;
                switch (e.Action)
                {
                    case LibraryItemAction.Add:
                        HistoryResults.Insert(0, e.Item);
                        if (IsHistoryShown) 
                            IsEmpty = HistoryResults.Count == 0;
                        break;
                    case LibraryItemAction.Update:
                        var item = HistoryResults.Where(i => i.MainId == e.Item.MainId && i.SecondaryId == e.Item.SecondaryId).FirstOrDefault();
                        if (item != null)
                        {
                            HistoryResults.Move(HistoryResults.IndexOf(item), 0);
                            item.LastPlayTime = e.Item.LastPlayTime;
                        }
                        break;
                    case LibraryItemAction.Remove:
                        item = HistoryResults.Where(i => i.MainId == e.Item.MainId && i.SecondaryId == e.Item.SecondaryId).FirstOrDefault();
                        if (item != null)
                        {
                            HistoryResults.Remove(item);
                            if (IsHistoryShown) 
                                IsEmpty = HistoryResults.Count == 0;
                        }
                        break;
                    default:
                        break;
                }
                IsLoading = false;
            });                
        }

        private async void Library_FavItemChanging(object sender, Models.Args.FavItemChangedEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (e.ItemType == MediaPlayType.None) return;
                IsLoading = true;
                MediaPlayType type = e.ItemType == MediaPlayType.RadioDemand ? MediaPlayType.RadioLive : e.ItemType;
                switch (e.Action)
                {
                    case LibraryItemAction.Add:
                        if (type == MediaPlayType.RadioLive && (FavRadio)e.Parameter != null)
                        {
                            FavRadioResults.Insert(0, (FavRadio)e.Parameter);
                            if (IsFavRadioShown)
                                IsEmpty = FavRadioResults.Count == 0;
                        }
                        else if ((FavContent)e.Parameter != null)
                        {
                            FavContentResults.Insert(0, (FavContent)e.Parameter);
                            if (IsFavContentShown)
                                IsEmpty = FavContentResults.Count == 0;
                        }
                        break;
                    case LibraryItemAction.Update:

                        break;
                    case LibraryItemAction.Remove:
                        if (type == MediaPlayType.RadioLive)
                        {
                            var radio = FavRadioResults.Where(i => i.MainId == e.MainId).FirstOrDefault();
                            if (radio != null)
                                FavRadioResults.Remove(radio);
                            if (IsFavRadioShown) 
                                IsEmpty = FavRadioResults.Count == 0;
                        }
                        else
                        {
                            var content = FavContentResults.Where(i => i.MainId == e.MainId).FirstOrDefault();
                            if (content != null)
                                FavContentResults.Remove(content);
                            if (IsFavContentShown) 
                                IsEmpty = FavContentResults.Count == 0;
                        }
                        break;
                    default:
                        break;
                }
                IsLoading = false;
            });                
        }

        private async void GetFavAndHistory()
        {
            IsItemsLoading = true;
            FavRadioResults.Clear();
            var radios = await library.GetFavRadios();
            foreach (var radio in radios)
                FavRadioResults.Insert(0, radio);
            FavContentResults.Clear();
            var contents = await library.GetFavContents();
            foreach (var content in contents)
                FavContentResults.Insert(0, content);
            HistoryResults.Clear();
            var histories = await library.GetHistories();
            foreach (var history in histories)
                HistoryResults.Insert(0, history);
            if (IsFavRadioShown)
                IsEmpty = FavRadioResults.Count == 0;
            else if (IsFavContentShown)
                IsEmpty = FavContentResults.Count == 0;
            else if (IsHistoryShown)
                IsEmpty = HistoryResults.Count == 0;
            IsItemsLoading = false;
        }

    }
}
