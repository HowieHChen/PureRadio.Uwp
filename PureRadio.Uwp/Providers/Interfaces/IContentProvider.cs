using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Providers.Interfaces
{
    /// <summary>
    /// 内容(专辑)数据支持
    /// </summary>
    public interface IContentProvider
    {
        /// <summary>
        /// 获取指定专辑的详细信息.
        /// </summary>
        /// <param name="contentId">需获取的专辑ID.</param>
        /// <param name="cancellationToken">异步中止令牌.</param>
        /// <returns>专辑详细信息</returns>
        Task<ContentInfoDetail> GetContentDetailInfo(int contentId, CancellationToken cancellationToken);

        /// <summary>
        /// 获取指定专辑的播放列表.
        /// </summary>
        /// <param name="contentId">需获取的专辑ID.</param>
        /// <param name="version">专辑版本号</param>
        /// <param name="cancellationToken">异步中止令牌.</param>
        /// <returns>专辑播放列表</returns>
        Task<List<ContentPlaylistDetail>> GetContentProgramListFull(int contentId, string version, CancellationToken cancellationToken);

        /// <summary>
        /// 获取指定分类和属性的电台结果.
        /// </summary>
        /// <param name="categoryId">分类Id.</param>
        /// <param name="cancellationToken">异步中止令牌.</param>
        /// <param name="attrId">属性Id.</param>
        /// <param name="page">页数.</param>
        /// <returns></returns>
        Task<ResultSet<ContentInfoCategory>> GetContentCategoryResult(int categoryId, CancellationToken cancellationToken, int attrId = 0, int page = 1);
    }
}
