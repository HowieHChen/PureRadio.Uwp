using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Data.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Providers.Interfaces
{
    /// <summary>
    /// 搜索操作
    /// </summary>
    public interface ISearchProvider
    {
        /// <summary>
        /// 获取搜索建议.
        /// </summary>
        /// <param name="keyword">搜索关键词.</param>
        /// <param name="cancellationToken">异步中止令牌.</param>
        /// <returns>搜索建议列表.</returns>
        Task<List<string>> GetSearchSuggestion(string keyword, CancellationToken cancellationToken);

        /// <summary>
        /// 获取电台搜索结果.
        /// </summary>
        /// <param name="keyword">搜索关键词.</param>
        /// <param name="cancellationToken">异步中止令牌.</param>
        /// <returns>用户搜索结果.</returns>
        Task<SearchSet<RadioInfoSearch>> GetRadioSearchResultAsync(string keyword, CancellationToken cancellationToken);

        /// <summary>
        /// 获取内容(专辑)搜索结果.
        /// </summary>
        /// <param name="keyword">搜索关键词.</param>
        /// <param name="cancellationToken">异步中止令牌.</param>
        /// <returns>文章搜索结果.</returns>
        Task<SearchSet<ContentInfoSearch>> GetContentSearchResultAsync(string keyword, CancellationToken cancellationToken);

        /// <summary>
        /// 重置电台搜索请求的状态.
        /// </summary>
        void ResetRadioStatus();

        /// <summary>
        /// 重置内容(专辑)搜索请求状态.
        /// </summary>
        void ResetContentStatus();

        /// <summary>
        /// 清空所有状态.
        /// </summary>
        void ClearStatus();
    }
}
