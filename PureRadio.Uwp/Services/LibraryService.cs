using Microsoft.Toolkit.Uwp.UI.Controls.TextToolbarSymbols;
using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Database;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.System;

namespace PureRadio.Uwp.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IPlaybackService _playbackService;
        private readonly ISqliteService _sqliteService;

        public event EventHandler<FavItemChangedEventArgs> FavItemChanging;
        public event EventHandler<HistoryItemChangedEventArgs> HistoryItemAdded;

        public LibraryService(
            IPlaybackService playbackService, 
            ISqliteService sqliteService)
        {
            _playbackService = playbackService;
            _sqliteService = sqliteService;
            _playbackService.PlayerItemChanged += AddItemToHistory;
        }

        private async void AddItemToHistory(object sender, PlayerItemChangedEventArgs e)
        {
            var item = e.Snapshot;
            string title;
            string subTitle;
            string localCover;
            LibraryItemAction action;
            if (item.Type == MediaPlayType.None) return;
            else if (item.Type == MediaPlayType.RadioLive)
            {
                var resourceLoader = new ResourceLoader();
                title = item.SubTitle;
                subTitle = resourceLoader.GetString("LangLiveNow");
            }
            else
            {
                title = item.Title;
                subTitle = item.SubTitle;
            }
            var history = _sqliteService.GetItems<History>(item.MainId).FirstOrDefault(i => i.SecondaryId == item.SecondaryId);
            if (history != null)
            {
                _ = await _sqliteService.DeleteAsync(history);
                if(history.Cover == item.Cover.OriginalString)
                {
                    localCover = history.LocalCover;
                }
                else
                {
                    await TryDeleteHistoryCoverAsync(false, history.LocalCover);
                    localCover = await DownloadCoverAsync(item.Type, item.MainId, item.Cover.OriginalString, true);
                }
                action = LibraryItemAction.Update;
            }
            else
            {
                localCover = await DownloadCoverAsync(item.Type, item.MainId, item.Cover.OriginalString, true);
                action = LibraryItemAction.Add;
            }             
            var historyItem = new History(
                item.Type, item.Cover.OriginalString, title, subTitle, item.MainId, item.SecondaryId, localCover, DateTime.Now);
            var addCount = await _sqliteService.UpsertAsync(historyItem);
            if (addCount > 0)
            {
                HistoryItemAdded?.Invoke(this, new HistoryItemChangedEventArgs(
                    action, historyItem));
            }
            if(await _sqliteService.GetCountAsync<History>() > 100)
            {
                var first = await _sqliteService.GetFirstItemAsync<History>();
                int deleteCount = await _sqliteService.DeleteAsync(first);
                await TryDeleteHistoryCoverAsync(false, first.LocalCover);
                HistoryItemAdded?.Invoke(this, new HistoryItemChangedEventArgs(
                    LibraryItemAction.Remove, first));
            }
        }

        public async Task<bool> AddToFav(PlayItemSnapshot item)
        {
            return item.Type switch
            {
                MediaPlayType.RadioLive => await AddRadioToFav(item),
                MediaPlayType.RadioDemand => await AddRadioToFav(item),
                MediaPlayType.ContentDemand => await AddContentToFav(item),
                _ => false,
            };
        }

        public async Task<bool> ClearHistory()
        {
            var deletedCount = await _sqliteService.ClearAsync<History>();
            if (deletedCount > 0)
            {
                await TryDeleteHistoryCoverAsync();
            }
            return deletedCount > 0;
        }

        public async Task<List<FavContent>> GetFavContents()
        {
            return await _sqliteService.GetItemsAsync<FavContent>() ?? new List<FavContent>();
        }

        public async Task<List<FavRadio>> GetFavRadios()
        {
            return await _sqliteService.GetItemsAsync<FavRadio>() ?? new List<FavRadio>();
        }

        public async Task<List<History>> GetHistories()
        {
            return await _sqliteService.GetItemsAsync<History>() ?? new List<History>();
        }

        public async Task<bool> IsFavItem(MediaPlayType type, int mainId)
        {
            return type switch
            {
                MediaPlayType.RadioLive => await _sqliteService.GetItemAsync<FavRadio>(mainId) != null,
                MediaPlayType.ContentDemand => await _sqliteService.GetItemAsync<FavContent>(mainId) != null,
                _ => false,
            };
        }

        public async Task<bool> IsFavItem(PlayItemSnapshot item)
        {
            if (item.Type == MediaPlayType.None) return false;
            return await IsFavItem(item.Type, item.MainId);
        }

        public async Task<bool> RemoveFromFav(PlayItemSnapshot item)
        {
            if (item.Type == MediaPlayType.None) return false;
            return await RemoveFromFav(item.Type, item.MainId);
        }

        public async Task<bool> RemoveFromFav(MediaPlayType mediaType, int mainId)
        {
            switch (mediaType)
            {
                default:
                case MediaPlayType.None:
                case MediaPlayType.RadioDemand:
                    break;
                case MediaPlayType.RadioLive:
                    FavRadio radio = await _sqliteService.GetItemAsync<FavRadio>(mainId);
                    if (radio != null)
                        return await RemoveFromFav(radio);
                    break;
                case MediaPlayType.ContentDemand:
                    FavContent content = await _sqliteService.GetItemAsync<FavContent>(mainId);
                    if (content != null)
                        return await RemoveFromFav(content);
                    break;
            }
            return false;
        }

        public async Task<bool> RemoveFromFav(FavRadio item)
        {
            var deletedCount = await _sqliteService.DeleteAsync(item);
            if (deletedCount > 0)
            {
                await TryDeleteCoverAsync(item.LocalCover);
                FavItemChanging?.Invoke(this, new FavItemChangedEventArgs(
                    LibraryItemAction.Remove, MediaPlayType.RadioLive, item.MainId, null));
            }                
            return deletedCount > 0;
        }

        public async Task<bool> RemoveFromFav(FavContent item)
        {
            var deletedCount = await _sqliteService.DeleteAsync(item);
            if (deletedCount > 0)
            {
                await TryDeleteCoverAsync(item.LocalCover);
                FavItemChanging?.Invoke(this, new FavItemChangedEventArgs(
                    LibraryItemAction.Remove, MediaPlayType.ContentDemand, item.MainId, null));
            }
            return deletedCount > 0;
        }

        private async Task<bool> AddRadioToFav(PlayItemSnapshot item)
        {
            if (await IsFavItem(MediaPlayType.RadioLive, item.MainId)) return true;
            var localCover = await DownloadCoverAsync(MediaPlayType.RadioLive, item.MainId, item.Cover.OriginalString);
            var radioItem = new FavRadio(
                item.MainId, item.SubTitle, item.Cover.OriginalString, localCover, DateTime.Now);
            var addCount = await _sqliteService.UpsertAsync(radioItem);
            if(addCount > 0)
            {
                FavItemChanging?.Invoke(this, new FavItemChangedEventArgs(
                    LibraryItemAction.Add, MediaPlayType.RadioLive, item.MainId, radioItem));
            }
            return addCount > 0;
        }

        private async Task<bool> AddContentToFav(PlayItemSnapshot item)
        {
            if (await IsFavItem(MediaPlayType.ContentDemand, item.MainId)) return true;
            var localCover = await DownloadCoverAsync(item.Type, item.MainId, item.Cover.OriginalString);
            var contentItem = new FavContent(
                item.Cover.OriginalString, item.MainId, item.SubTitle, localCover, DateTime.Now);
            var addCount = await _sqliteService.UpsertAsync(contentItem);
            if (addCount > 0)
            {
                FavItemChanging?.Invoke(this, new FavItemChangedEventArgs(
                    LibraryItemAction.Add, MediaPlayType.ContentDemand, item.MainId, contentItem));
            }
            return addCount > 0;
        }

        private async Task<string> DownloadCoverAsync(MediaPlayType type, int mainId, string coverUrl, bool isHistory = false)
        {
            if (string.IsNullOrEmpty(coverUrl))
            {
                return string.Empty;
            }

            StorageFolder folder;
            if (isHistory)
            {
                folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("History", CreationCollisionOption.OpenIfExists);
            }
            else
                folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Cover", CreationCollisionOption.OpenIfExists);

            string fileName = $"{DateTime.Now:yyyy-MM-dd}-{type}-{mainId}-{Guid.NewGuid():N}.png";
            using (var httpClient = new HttpClient())
            {    
                var bytes = await httpClient.GetByteArrayAsync(new Uri(coverUrl));
                var imgFile = await folder.CreateFileAsync(fileName);
                await FileIO.WriteBytesAsync(imgFile, bytes);
            }
            return isHistory ? $"ms-appdata:///local/History/{fileName}" : $"ms-appdata:///local/Cover/{fileName}";
        }

        private async Task TryDeleteCoverAsync(string fileUrl)
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Cover");
                var item = await folder.TryGetItemAsync(fileUrl.Replace("ms-appdata:///local/Cover/", string.Empty));
                if (item != null)
                    await item.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
            catch (FileNotFoundException)
            {

            }            
        }

        private async Task TryDeleteHistoryCoverAsync(bool deleteAll = true, string fileUrl = "")
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("History");

                if (!deleteAll)
                {
                    var item = await folder.TryGetItemAsync(fileUrl.Replace("ms-appdata:///local/History/", string.Empty));
                    if (item != null)
                        await item.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
                else
                {
                    await folder.DeleteAsync();
                }
            }
            catch (FileNotFoundException)
            {

            }
        }
    }
}
