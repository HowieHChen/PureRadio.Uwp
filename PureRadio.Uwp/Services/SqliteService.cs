using Microsoft.Toolkit.Uwp.UI.Animations;
using PureRadio.Uwp.Models.Database;
using PureRadio.Uwp.Services.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PureRadio.Uwp.Services
{
    public class SqliteService : ISqliteService
    {
        public static readonly string DbName = "pure_radio.db";

        /// <summary>
        /// 数据库路径
        /// </summary>
        public readonly string DbPath = 
            Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);

        /// <summary>
        /// 数据库同步连接
        /// </summary>
        private SQLiteConnection _db;
        /// <summary>
        /// 数据库异步连接
        /// </summary>
        private SQLiteAsyncConnection _asyncDb;
        /// <summary>
        /// 数据库更新队列
        /// </summary>
        private Queue<DbObject> _upsertQueue;

        public async Task InitializeDatabaseAsync()
        {
            _ = await ApplicationData.Current.LocalFolder.CreateFileAsync(DbName, CreationCollisionOption.OpenIfExists);

            _db ??= new SQLiteConnection(DbPath);
            _asyncDb ??= new SQLiteAsyncConnection(DbPath);

            _ = await _asyncDb.CreateTableAsync<FavRadio>();
            _ = await _asyncDb.CreateTableAsync<FavContent>();
            _ = await _asyncDb.CreateTableAsync<History>();
            
            _upsertQueue ??= new();
        }

        public List<T> GetItems<T>() where T : DbObject, new()
        {
            var table = _db.Table<T>();
            return table.ToList();
        }

        public Task<List<T>> GetItemsAsync<T>() where T : DbObject, new()
        {
            var table = _asyncDb.Table<T>();
            return table.ToListAsync();
        }

        public List<T> GetItems<T>(int mainId) where T : DbObject, new()
            => GetItems<T>().AsParallel().Where(i => i.MainId == mainId).ToList();

        public async Task<List<T>> GetItemsAsync<T>(int mainId) where T : DbObject, new()
            => (await GetItemsAsync<T>()).AsParallel().Where(i => i.MainId == mainId).ToList();

        public T GetItem<T>(Guid id) where T : DbObject, new()
            => GetItems<T>().AsParallel().FirstOrDefault(i => i.Id == id);

        public async Task<T> GetItemAsync<T>(Guid id) where T : DbObject, new()
            => (await GetItemsAsync<T>()).AsParallel().FirstOrDefault(i => i.Id == id);

        public T GetItem<T>(int mainId) where T : DbObject, new()
            => GetItems<T>().AsParallel().FirstOrDefault(i => i.MainId == mainId);

        public async Task<T> GetItemAsync<T>(int mainId) where T : DbObject, new()
            => (await GetItemsAsync<T>()).AsParallel().FirstOrDefault(i => i.MainId == mainId);

        public int GetCount<T>() where T : DbObject, new()
            => GetItems<T>().AsParallel().Count();

        public async Task<int> GetCountAsync<T>() where T : DbObject, new()
            => (await GetItemsAsync<T>()).AsParallel().Count();

        public T GetFirstItem<T>() where T : DbObject, new()
            => GetItems<T>().AsParallel().FirstOrDefault();

        public async Task<T> GetFirstItemAsync<T>() where T : DbObject, new()
            => (await GetItemsAsync<T>()).AsParallel().FirstOrDefault();

        public T GetLastItem<T>() where T : DbObject, new()
            => GetItems<T>().AsParallel().LastOrDefault();

        public async Task<T> GetLastItemAsync<T>() where T : DbObject, new()
            => (await GetItemsAsync<T>()).AsParallel().LastOrDefault();

        public int Delete(DbObject item)
        {
            return _db.Delete(item);
        }

        public Task<int> DeleteAsync(DbObject item)
        {
            return _asyncDb.DeleteAsync(item);
        }

        public int Upsert(DbObject item)
        {
            int result = _db.InsertOrReplace(item);
            return result;
        }

        public async Task<int> UpsertAsync(DbObject item)
        {
            int result = await _asyncDb.InsertOrReplaceAsync(item);
            return result;
        }

        public bool QueueUpsert(DbObject item)
        {
            if (!_upsertQueue.Contains(item))
            {
                _upsertQueue.Enqueue(item);
                return true;
            }
            return false;
        }

        public async Task UpsertQueuedAsync()
        {
            foreach (var item in _upsertQueue)
            {
                _ = await _asyncDb.InsertOrReplaceAsync(item);
            }
        }

        public int Clear<T>() where T : DbObject, new()
        {
            return _db.DeleteAll<T>();
        }

        public Task<int> ClearAsync<T>() where T : DbObject, new()
        {
            return _asyncDb.DeleteAllAsync<T>();
        }
    }
}
