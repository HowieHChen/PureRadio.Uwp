using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Database;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Services.Interfaces
{
    /// <summary>
    /// 库服务
    /// </summary>
    public interface ILibraryService
    {
        /// <summary>
        /// 收藏状态变化事件
        /// </summary>
        event EventHandler<FavItemChangedEventArgs> FavItemChanging;
        /// <summary>
        /// 历史播放项目增加事件
        /// </summary>
        event EventHandler<HistoryItemChangedEventArgs> HistoryItemAdded;
        /// <summary>
        /// 查询项目是否在收藏夹中
        /// </summary>
        /// <param name="type">收藏类型</param>
        /// <param name="mainId">一级ID</param>
        /// <returns>是否在收藏夹中</returns>
        Task<bool> IsFavItem(MediaPlayType type, int mainId);
        /// <summary>
        /// 查询项目是否在收藏夹中
        /// </summary>
        /// <param name="mainId">一级ID</param>
        /// <returns>是否在收藏夹中</returns>
        Task<bool> IsFavItem(PlayItemSnapshot item);
        /// <summary>
        /// 将项目添加到收藏夹
        /// </summary>
        /// <param name="item">项目</param>
        /// <returns>操作是否成功</returns>
        Task<bool> AddToFav(PlayItemSnapshot item);
        /// <summary>
        /// 将项目从收藏夹中移出
        /// </summary>
        /// <param name="item">项目</param>
        /// <returns>操作是否成功</returns>
        Task<bool> RemoveFromFav(PlayItemSnapshot item);
        /// <summary>
        /// 将项目从收藏夹中移出
        /// </summary>
        /// <param name="item">项目</param>
        /// <returns>操作是否成功</returns>
        Task<bool> RemoveFromFav(FavRadio item);
        /// <summary>
        /// 将项目从收藏夹中移出
        /// </summary>
        /// <param name="item">项目</param>
        /// <returns>操作是否成功</returns>
        Task<bool> RemoveFromFav(FavContent item);
        /// <summary>
        /// 将项目从收藏夹中移出
        /// </summary>
        /// <param name="mediaType">项目类型</param>
        /// <param name="mainId">一级Id</param>
        /// <returns>操作是否成功</returns>
        Task<bool> RemoveFromFav(MediaPlayType mediaType, int mainId);
        /// <summary>
        /// 清空播放历史记录
        /// </summary>
        /// <returns>操作是否成功</returns>
        Task<bool> ClearHistory();
        /// <summary>
        /// 获取收藏夹电台列表
        /// </summary>
        /// <returns>收藏夹电台列表</returns>
        Task<List<FavRadio>> GetFavRadios();
        /// <summary>
        /// 获取收藏夹内容列表
        /// </summary>
        /// <returns>收藏夹内容列表</returns>
        Task<List<FavContent>> GetFavContents();
        /// <summary>
        /// 获取播放记录列表
        /// </summary>
        /// <returns>播放记录列表</returns>
        Task<List<History>> GetHistories();

    }
}
