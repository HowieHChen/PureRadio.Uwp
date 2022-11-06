using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.QingTing.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Adapters.Interfaces
{
    /// <summary>
    /// 搜索数据适配器接口定义.
    /// </summary>
    public interface ISearchAdapter
    {
        /// <summary>
        /// 将来自 Web 的搜索建议条目 <see cref="SearchSuggestItem"/> 转换为本地搜索建议条目(string).
        /// </summary>
        /// <param name="item">来自 Web 的搜索建议条目.</param>
        /// <returns><see cref="string"/>.</returns>
        string ConvertToSearchSuggest(SearchSuggestItem item);
        /// <summary>
        /// 将来自 Web 的电台搜索结果 <see cref="SearchRadioItem"/> 转换为电台搜索结果视图模型 <see cref="RadioInfoSearch"/>.
        /// </summary>
        /// <param name="item">来自 Web 的电台搜索结果.</param>
        /// <returns>电台搜索结果视图模型 <see cref="RadioInfoSearch"/>.</returns>
        RadioInfoSearch ConvertToSearchResultView(SearchRadioItem item);
        /// <summary>
        /// 将来自 Web 的电台搜索结果 <see cref="SearchContentItem"/> 转换为电台搜索结果视图模型 <see cref="ContentInfoSearch"/>.
        /// </summary>
        /// <param name="item">来自 Web 的电台搜索结果.</param>
        /// <returns>电台搜索结果视图模型 <see cref="ContentInfoSearch"/>.</returns>
        ContentInfoSearch ConvertToSearchResultView(SearchContentItem item);
    }
}
