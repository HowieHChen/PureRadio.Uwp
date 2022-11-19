using PureRadio.Uwp.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Services.Interfaces
{
    public interface ISqliteService
    {
        /// <summary>
        /// 初始化数据库和表
        /// </summary>
        /// <returns>异步状态</returns>
        Task InitializeDatabaseAsync();

        /// <summary>
        /// 获取表里的所有项目
        /// </summary>
        /// <typeparam name="T">表的类型</typeparam>
        /// <returns>表内的所有项目</returns>
        List<T> GetItems<T>() where T : DbObject, new();

        /// <summary>
        /// 获取表里的所有项目
        /// </summary>
        /// <typeparam name="T">表的类型</typeparam>
        /// <returns>表内的所有项目</returns>
        Task<List<T>> GetItemsAsync<T>() where T : DbObject, new();

        /// <summary>
        /// 获取具有指定一级Id的所有项目
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <param name="id">一级Id</param>
        /// <returns>所有匹配项目</returns>
        List<T> GetItems<T>(int mainId) where T : DbObject, new();

        /// <summary>
        /// 获取具有指定一级Id的所有项目
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <param name="id">一级Id</param>
        /// <returns>所有匹配项目</returns>
        Task<List<T>> GetItemsAsync<T>(int mainId) where T : DbObject, new();

        /// <summary>
        /// 将项目更新到数据库
        /// </summary>
        /// <param name="item">更新的项目</param>
        /// <returns>修改的行数</returns>
        int Upsert(DbObject item);

        /// <summary>
        /// 将项目更新到数据库
        /// </summary>
        /// <param name="item">更新的项目</param>
        /// <returns>修改的行数</returns>
        Task<int> UpsertAsync(DbObject item);

        /// <summary>
        /// 将项目提交到数据库更新队列
        /// </summary>
        /// <param name="item">更新的项目</param>
        /// <returns>操作是否成功</returns>
        bool QueueUpsert(DbObject item);

        /// <summary>
        /// 将更新队列里的项目全部提交到数据库
        /// </summary>
        /// <returns>异步状态</returns>
        Task UpsertQueuedAsync();

        /// <summary>
        /// 从数据库中删除一个项目
        /// </summary>
        /// <param name="item">删除的项目</param>
        /// <returns>修改的行数</returns>
        int Delete(DbObject item);

        /// <summary>
        /// 从数据库中删除一个项目
        /// </summary>
        /// <param name="item">删除的项目</param>
        /// <returns>修改的行数</returns>
        Task<int> DeleteAsync(DbObject item);

        /// <summary>
        /// 删除表里的所有项目
        /// </summary>
        /// <typeparam name="T">表的类型</typeparam>
        /// <returns>修改的行数</returns>
        int Clear<T>() where T : DbObject, new();

        /// <summary>
        /// 删除表里的所有项目
        /// </summary>
        /// <typeparam name="T">表的类型</typeparam>
        /// <returns>修改的行数</returns>
        Task<int> ClearAsync<T>() where T : DbObject, new();

        /// <summary>
        /// 获取具有指定Guid的项目
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <param name="id">GUID</param>
        /// <returns>项目，若不存在则返回Null</returns>
        T GetItem<T>(Guid id) where T : DbObject, new();

        /// <summary>
        /// 获取具有指定Guid的项目
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <param name="id">GUID</param>
        /// <returns>项目，若不存在则返回Null</returns>
        Task<T> GetItemAsync<T>(Guid id) where T : DbObject, new();

        /// <summary>
        /// 获取具有指定一级Id的项目
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <param name="mainId">一级Id</param>
        /// <returns>项目，若不存在则返回Null</returns>
        T GetItem<T>(int mainId) where T : DbObject, new();

        /// <summary>
        /// 获取具有指定一级Id的项目
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <param name="mainId">一级Id</param>
        /// <returns>项目，若不存在则返回Null</returns>
        Task<T> GetItemAsync<T>(int mainId) where T : DbObject, new();

        /// <summary>
        /// 获取表内第一项
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <returns>项目，若不存在则返回Null</returns>
        T GetFirstItem<T>() where T : DbObject, new();

        /// <summary>
        /// 获取表内第一项
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <returns>项目，若不存在则返回Null</returns>
        Task<T> GetFirstItemAsync<T>() where T : DbObject, new();

        /// <summary>
        /// 获取表内最后一项
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <returns>项目，若不存在则返回Null</returns>
        T GetLastItem<T>() where T : DbObject, new();

        /// <summary>
        /// 获取表内最后一项
        /// </summary>
        /// <typeparam name="T">项目类型</typeparam>
        /// <returns>项目，若不存在则返回Null</returns>
        Task<T> GetLastItemAsync<T>() where T : DbObject, new();

        /// <summary>
        /// 获取表里的项目的个数
        /// </summary>
        /// <typeparam name="T">表的类型</typeparam>
        /// <returns>总个数</returns>
        int GetCount<T>() where T : DbObject, new();

        /// <summary>
        /// 获取表里的项目的个数
        /// </summary>
        /// <typeparam name="T">表的类型</typeparam>
        /// <returns>总个数</returns>
        Task<int> GetCountAsync<T>() where T : DbObject, new();
    }
}
