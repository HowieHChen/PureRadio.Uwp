using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.QingTing.Content;
using PureRadio.Uwp.Models.QingTing.Radio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Adapters.Interfaces
{
    /// <summary>
    /// 内容(专辑)数据适配器定义
    /// </summary>
    public interface IContentAdapter
    {
        /// <summary>
        /// 将来自 Web 的专辑详情条目 <see cref="ContentDetailItem"/> 转换为本地专辑详情条目 <see cref="ContentInfoDetail"/> .
        /// </summary>
        /// <param name="item">来自 Web 的专辑详情条目.</param>
        /// <param name="attributes">来自 Web 的专辑属性标签.</param>
        /// <returns><see cref="ContentInfoDetail"/>.</returns>
        ContentInfoDetail ConvertToContentInfoDetail(ContentDetailItem item, List<AttributesItem> attributes = null);

        /// <summary>
        /// 将来自 Web 的专辑节目列表项 <see cref="ContentProgramListItem"/> 转换为本地专辑节目列表项 <see cref="ContentPlaylistDetail"/> .
        /// </summary>
        /// <param name="item">将来自 Web 的专辑节目列表项.</param>
        /// <param name="version">将来自 Web 的专辑节目版本号.</param>
        /// <returns><see cref="ContentPlaylistDetail"/>.</returns>
        ContentPlaylistDetail ConvertToContentPlaylistItem(ContentProgramListItem item, string version);

        /// <summary>
        /// 将来自 Web 的分类专辑节目列表项 <see cref="ContentCategoryItem"/> 转换为本地分类专辑节目列表项 <see cref="ContentInfoCategory"/> .
        /// </summary>
        /// <param name="item">来自 Web 的分类专辑节目列表项.</param>
        /// <returns><see cref="ContentInfoCategory"/>.</returns>
        ContentInfoCategory ConvertToContentInfoCategory(ContentCategoryItem item);
    }
}
