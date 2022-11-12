using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
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
    }
}
